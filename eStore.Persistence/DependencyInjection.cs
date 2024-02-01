using eStore.Application.Common.Authentication;
using eStore.Application.Common.Interfaces;
using eStore.Application.RepositoriesInterface;
using eStore.Domain.Entities;
using eStore.Infrastructure.Identity;
using eStore.Infrastructure.Persistence.Interceptors;
using eStore.Infrastructure.Services;
using eStore.Persistence.Authentication;
using eStore.Persistence.DbContext;
using eStore.Persistence.Repositories;
using eStore.Persistence.SeedDatabase;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eStore.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddScoped<AuditableEntitySaveChangesInterceptor>();
            services.AddScoped<Seed>();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                );
            });

            services.AddScoped<IApplicationDbContext>(
                provider => provider.GetRequiredService<ApplicationDbContext>()
            );

            var builder = services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequiredLength = 5;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredUniqueChars = 0;
            });

            // Need to check here
            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager<SignInManager<AppUser>>()
                .AddRoleManager<RoleManager<Role>>()
                .AddRoleValidator<RoleValidator<Role>>()
                .AddDefaultTokenProviders();

            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IIdentityService, IdentityService>();

            //Repo
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            services.AddScoped<IMaterialRepository, MaterialRepository>();

            return services;
        }
    }
}
