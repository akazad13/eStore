using AutoMapper;
using Exino.Application.Common.Behaviours;
using Exino.Application.Common.Mapper;
using Exino.Application.Common.Services.AWSS3;
using Exino.Application.Common.Utilities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Exino.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

            services.AddSingleton(context => AddAutoMapper(new AppDomainTypeFinder()));

            //Add helper and other services
            services.AddScoped<IHelper, Helper>();
            services.AddScoped<IAWSS3Service, AWSS3Service>();

            return services;
        }

        private static IMapper AddAutoMapper(ITypeFinder typeFinder)
        {
            var mapperConfigurations = typeFinder.FindClassesOfType<IOrderedMapperProfile>();

            //create and sort instances of mapper configurations
            var instances = mapperConfigurations
                .Select(
                    mapperConfiguration =>
                        (IOrderedMapperProfile)Activator.CreateInstance(mapperConfiguration)
                )
                .OrderBy(mapperConfiguration => mapperConfiguration.Order);

            //create AutoMapper configuration
            var config = new MapperConfiguration(cfg =>
            {
                foreach (var instance in instances)
                {
                    cfg.AddProfile(instance.GetType());
                }
            });

            //register
            AutoMapperConfiguration.Init(config);

            return AutoMapperConfiguration.Mapper;
        }
    }
}
