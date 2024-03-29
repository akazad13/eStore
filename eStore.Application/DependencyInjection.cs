﻿using AutoMapper;
using eStore.Application.Common.Behaviours;
using eStore.Application.Common.Mapper;
using eStore.Application.Common.Services.AWSS3;
using eStore.Application.Common.Utilities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace eStore.Application
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
            services.AddScoped<IAwsS3Service, AwsS3Service>();

            return services;
        }

        private static IMapper AddAutoMapper(ITypeFinder typeFinder)
        {
            var mapperConfigurations = typeFinder.FindClassesOfType<IOrderedMapperProfile>();
            if (mapperConfigurations != null)
            {
                //create and sort instances of mapper configurations
                var instances = mapperConfigurations
                    .Select(
                        mapperConfiguration =>
                            (IOrderedMapperProfile)Activator.CreateInstance(mapperConfiguration)!
                    )
                    .OrderBy(mapperConfiguration => mapperConfiguration!.Order);

                //create AutoMapper configuration
                var config = new MapperConfiguration(cfg =>
                {
                    foreach (var instance in instances)
                    {
                        cfg.AddProfile(instance!.GetType());
                    }
                });
                //register
                AutoMapperConfiguration.Init(config);
            }
            return AutoMapperConfiguration.Mapper!;
        }
    }
}
