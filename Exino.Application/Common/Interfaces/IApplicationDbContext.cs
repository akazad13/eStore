using Exino.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Exino.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        //DbSet<Address> Addresses { get; set; }
        //DbSet<Basket> Baskets { get; set; }
        //DbSet<BasketItem> BasketItems { get; set; }
        //DbSet<Category> Categories { get; set; }
        //DbSet<Material> Materials { get; set; }
        //DbSet<Product> Products { get; set; }
        //DbSet<Order> Orders { get; set; }
        //DbSet<OrderDetail> OrderDetails { get; set; }
        DbSet<T> GetDbSet<T>() where T : class;
        //DbSet<ProductComment> ProductComments { get; set; }
        //DbSet<ProductRating> ProductRatings { get; set; }
        //DbSet<ProductMaterial> ProductMaterials { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
