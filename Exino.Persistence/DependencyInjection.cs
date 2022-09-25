using Exino.Application.Common.Authentication;
using Exino.Application.Common.Interfaces;
using Exino.Application.RepositoriesInterface;
using Exino.Domain.Entities;
using Exino.Infrastructure.Identity;
using Exino.Infrastructure.Persistence.Interceptors;
using Exino.Infrastructure.Services;
using Exino.Persistence.Authentication;
using Exino.Persistence.DbContext;
using Exino.Persistence.Repositories;
using Exino.Persistence.SeedDatabase;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Exino.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddScoped<AuditableEntitySaveChangesInterceptor>();
            services.AddScoped<Seed>();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            });

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            var builder = services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequiredLength = 5;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredUniqueChars = 0;
            });

            // Need to check here
            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager<SignInManager<AppUser>>()
                .AddRoleManager<RoleManager<Role>>()
                .AddRoleValidator<RoleValidator<Role>>()
                .AddDefaultTokenProviders();


            services.AddScoped<IJWTTokenGenerator, JWTTokenGenerator>();
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IIdentityService, IdentityService>();

            //Repo
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductImageRepository, ProductImageRepository>();


            return services;
        }
    }
}