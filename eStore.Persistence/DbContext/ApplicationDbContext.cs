using eStore.Application.Common.Interfaces;
using eStore.Domain.Entities;
using eStore.Infrastructure.Persistence.Interceptors;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eStore.Persistence.DbContext
{
    public class ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor
        )
                : IdentityDbContext<
            AppUser,
            Role,
            long,
            IdentityUserClaim<long>,
            AppUserRole,
            IdentityUserLogin<long>,
            IdentityRoleClaim<long>,
            IdentityUserToken<long>
        >(options),
            IApplicationDbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<ProductRating> ProductRatings { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

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

                u.Property(x => x.Birthdate).HasColumnOrder(7);
                u.Property(x => x.Gender).HasColumnOrder(8);
                u.Property(x => x.CreatedOn).HasColumnOrder(9);
                u.Property(x => x.CreatedBy).HasColumnOrder(10);
                u.Property(x => x.ModifiedOn).HasColumnOrder(11);
                u.Property(x => x.ModifiedBy).HasColumnOrder(12);
                u.Property(x => x.Status).HasColumnOrder(13);
                u.Property(x => x.ImagePath).IsRequired(false).HasColumnOrder(14);
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

                userRole
                    .HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
                userRole
                    .HasOne(ur => ur.AppUser)
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

            builder.Entity<Address>(a =>
            {
                a.HasOne(x => x.AppUser).WithMany(x => x.Addresses).HasForeignKey(x => x.UserID);
            });

            builder.Entity<Basket>(b =>
            {
                b.Property(x => x.Subtotal)
                    .HasPrecision(18, 5)
                    .HasConversion<decimal>()
                    .IsRequired();
                b.HasOne(x => x.AppUser).WithMany(x => x.Baskets).HasForeignKey(x => x.UserId);
            });

            builder.Entity<BasketItem>(bi =>
            {
                bi.Property(x => x.Quantity).IsRequired();
                bi.Property(x => x.Price).HasPrecision(18, 5).HasConversion<decimal>().IsRequired();
                bi.Property(x => x.UnitPrice)
                    .HasPrecision(18, 5)
                    .HasConversion<decimal>()
                    .IsRequired();
                bi.HasOne(x => x.Product)
                    .WithMany(x => x.BasketItems)
                    .HasForeignKey(x => x.ProductID);
                bi.HasOne(x => x.Basket)
                    .WithMany(x => x.BasketItems)
                    .HasForeignKey(x => x.BasketId);
            });

            builder.Entity<Category>(c =>
            {
                c.Property(x => x.Name).HasMaxLength(100).IsRequired();
            });

            builder.Entity<Order>(o =>
            {
                o.Property(x => x.Amount).HasPrecision(18, 5).HasConversion<decimal>().IsRequired();
                o.Property(x => x.ShippingAddress).IsRequired();
                o.HasOne(x => x.AppUser).WithMany(x => x.Orders).HasForeignKey(x => x.UserId);
            });

            builder.Entity<OrderDetail>(od =>
            {
                od.Property(x => x.Quantity).IsRequired();
                od.Property(x => x.Price).HasPrecision(18, 5).HasConversion<decimal>().IsRequired();

                od.HasOne(x => x.Product)
                    .WithMany(x => x.OrdeDetails)
                    .HasForeignKey(x => x.ProductId);

                od.HasOne(x => x.Order).WithMany(x => x.OrderDetails).HasForeignKey(x => x.OrderId);
            });

            builder.Entity<Product>(p =>
            {
                p.Property(x => x.Name).HasMaxLength(200).IsRequired();
                p.Property(x => x.Description).IsRequired();
                p.Property(x => x.Size).IsRequired();
                p.Property(x => x.Color).IsRequired();
                p.Property(x => x.Stock).IsRequired();
                p.Property(x => x.Price).HasPrecision(18, 5).HasConversion<decimal>().IsRequired();

                p.HasOne(x => x.Category)
                    .WithMany(x => x.Products)
                    .HasForeignKey(x => x.CategoryId);

                p.HasOne(x => x.Material)
                    .WithMany(x => x.Products)
                    .HasForeignKey(x => x.MaterialId);
            });

            builder.Entity<ProductComment>(pc =>
            {
                pc.HasOne(x => x.AppUser)
                    .WithMany(x => x.ProductComments)
                    .HasForeignKey(x => x.UserId)
                    .IsRequired(false);

                pc.HasOne(x => x.Product)
                    .WithMany(x => x.ProductComments)
                    .HasForeignKey(x => x.ProductId);

                pc.Property(x => x.Text).HasMaxLength(500).IsRequired();
            });

            builder.Entity<ProductRating>(pr =>
            {
                pr.HasOne(x => x.AppUser)
                    .WithMany(x => x.ProductRatings)
                    .HasForeignKey(x => x.UserId)
                    .IsRequired(false);

                pr.HasOne(x => x.Product)
                    .WithMany(x => x.ProductRatings)
                    .HasForeignKey(x => x.ProductId);

                pr.Property(x => x.Rating)
                    .HasPrecision(18, 5)
                    .HasConversion<decimal>()
                    .IsRequired();
            });

            builder.Entity<Material>(m =>
            {
                m.Property(x => x.Name).HasMaxLength(50).IsRequired();
            });

            builder.Entity<ProductImage>(pi =>
            {
                pi.HasOne(x => x.Product)
                    .WithMany(x => x.ProductImages)
                    .HasForeignKey(x => x.ProductId);
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(auditableEntitySaveChangesInterceptor);
        }

        public DbSet<T> GetDbSet<T>() where T : class
        {
            return this.Set<T>();
        }

        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default
        )
        {
            await mediator.DispatchDomainEvents(this);

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
