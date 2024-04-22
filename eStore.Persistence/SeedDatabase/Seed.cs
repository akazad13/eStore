using eStore.Domain.Entities;
using eStore.Domain.Enums;
using eStore.Persistence.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eStore.Persistence.SeedDatabase
{
    public class Seed(
        ILogger<Seed> logger,
        ApplicationDbContext context,
        UserManager<AppUser> userManager,
        RoleManager<Role> roleManager
        )
    {
        public async Task InitialiseAsync()
        {
            try
            {
                if (context.Database.IsSqlServer())
                {
                    await context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            if (!await userManager.Users.AnyAsync())
            {
                var roles = new List<Role>
                {
                    new() {
                        Name = "Admin",
                        CreatedBy = 1,
                        CreatedOn = DateTime.UtcNow
                    },
                    new() {
                        Name = "Customer",
                        CreatedBy = 1,
                        CreatedOn = DateTime.UtcNow
                    },
                    new() {
                        Name = "Manager",
                        CreatedBy = 1,
                        CreatedOn = DateTime.UtcNow
                    }
                };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }

                var admin = new AppUser
                {
                    UserName = "admin@eStore.com",
                    Email = "admin@eStore.com",
                    FirstName = "eStore",
                    LastName = "Admin",
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = 0,
                    Status = Status.Active
                };

                var result = await userManager.CreateAsync(admin, "Password123");
                if (result.Succeeded)
                {
                    await userManager.AddToRolesAsync(admin, ["Admin"]);
                }
            }
        }
    }
}
