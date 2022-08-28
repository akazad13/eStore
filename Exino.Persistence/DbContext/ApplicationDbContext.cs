using Exino.Application.Common.Interfaces;
using Exino.Domain.Entities;
using Exino.Infrastructure.Persistence.Interceptors;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Exino.Persistence.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, Role, long, IdentityUserClaim<long>, AppUserRole, IdentityUserLogin<long>, IdentityRoleClaim<long>, IdentityUserToken<long>>, IApplicationDbContext
    {
        private readonly IMediator _mediator;
        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IMediator mediator,
            AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor
        ) : base(options)
        {
            _mediator = mediator;
            _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>(u =>
            {
                u.ToTable(name: "AppUsers");

                u.Property(u => u.Id).HasColumnOrder(0);
                u.Property(x => x.UserName).IsRequired().HasMaxLength(50).HasColumnOrder(1);
                u.Property(x => x.FirstName).IsRequired().HasMaxLength(50).HasColumnOrder(2);
                u.Property(x => x.LastName).IsRequired().HasMaxLength(50).HasColumnOrder(3);
                u.Property(x => x.Email).IsRequired().HasMaxLength(50).HasColumnOrder(4);
                u.Property(x => x.EmailConfirmed).IsRequired().HasMaxLength(50).HasColumnOrder(5);
                u.Property(x => x.PhoneNumber).HasMaxLength(20).HasColumnOrder(6);

                u.Property(x => x.NormalizedUserName).HasMaxLength(20);
                u.Property(x => x.NormalizedEmail).HasMaxLength(50);

                u.Property(x => x.CreatedOn).HasColumnOrder(7);
                u.Property(x => x.CreatedBy).HasColumnOrder(8);
                u.Property(x => x.ModifiedOn).HasColumnOrder(9);
                u.Property(x => x.ModifiedBy).HasColumnOrder(10);
                u.Property(x => x.Status).HasColumnOrder(11);
                u.Property(x => x.ImagePath).IsRequired(false).HasColumnOrder(12);
            });

            builder.Entity<Role>(r =>
            {
                r.ToTable(name: "Roles");
                r.Property(x => x.Id).HasColumnOrder(0);
                r.Property(x => x.Name).HasMaxLength(20).HasColumnOrder(1);
                r.Property(x => x.NormalizedName).HasMaxLength(20);

                r.Property(x => x.CreatedOn).HasColumnOrder(2);
                r.Property(x => x.CreatedBy).HasColumnOrder(3);
                r.Property(x => x.ModifiedOn).HasColumnOrder(4);
                r.Property(x => x.ModifiedBy).HasColumnOrder(5);
            });


            builder.Entity<AppUserRole>(userRole =>
            {
                userRole.ToTable(name: "UserRoles");
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
                userRole.HasOne(ur => ur.AppUser)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });


            builder.Entity<IdentityUserClaim<long>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<long>>(entity =>
            {
                entity.ToTable("UserLogins");
                entity.HasIndex(x => new { x.ProviderKey, x.LoginProvider });
            });

            builder.Entity<IdentityRoleClaim<long>>(entity =>
            {
                entity.ToTable("RoleClaims");

            });

            builder.Entity<IdentityUserToken<long>>(entity =>
            {
                entity.ToTable("UserTokens");
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEvents(this);

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
