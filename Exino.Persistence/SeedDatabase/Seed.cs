using Exino.Domain.Entities;
using Exino.Domain.Enums;
using Exino.Persistence.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Exino.Persistence.SeedDatabase
{
    public class Seed
    {
        private readonly ILogger<Seed> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public Seed(
            ILogger<Seed> logger,
            ApplicationDbContext context,
            UserManager<AppUser> userManager,
            RoleManager<Role> roleManager
        )
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
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
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            if (!await _userManager.Users.AnyAsync())
            {
                var roles = new List<Role>
                {
                    new Role
                    {
                        Name = "Admin",
                        CreatedBy = 1,
                        CreatedOn = DateTime.UtcNow
                    },
                    new Role
                    {
                        Name = "Customer",
                        CreatedBy = 1,
                        CreatedOn = DateTime.UtcNow
                    },
                    new Role
                    {
                        Name = "Manager",
                        CreatedBy = 1,
                        CreatedOn = DateTime.UtcNow
                    }
                };

                foreach (var role in roles)
                {
                    await _roleManager.CreateAsync(role);
                }

                var admin = new AppUser
                {
                    UserName = "admin@exino.com",
                    Email = "admin@exino.com",
                    FirstName = "Exino",
                    LastName = "Admin",
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = 0,
                    Status = Status.Active
                };

                var result = await _userManager.CreateAsync(admin, "Password123");
                if (result.Succeeded)
                {
                    await _userManager.AddToRolesAsync(admin, new[] { "Admin" });
                }
            }
        }
    }
}
