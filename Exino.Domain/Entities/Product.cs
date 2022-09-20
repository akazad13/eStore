using Exino.Domain.Common;

namespace Exino.Domain.Entities
{
    public class Product : BaseAuditableEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Size { get; set; }
        public string? Color { get; set; }
        public string? SKU { get; set; }

        public string? ImagePath { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public ICollection<OrderDetail>? OrdeDetails { get; set; }
        public ICollection<BasketItem>? BasketItems { get; set; }
        public ICollection<ProductComment>? ProductComments { get; set; }
        public ICollection<ProductRating>? ProductRatings { get; set; }
        public ICollection<ProductMaterial>? ProductMaterials { get; set; }
    }
}
