using eStore.Domain.Common;

namespace eStore.Domain.Entities
{
    public class ProductImage : BaseAuditableEntity
    {
        public long Id { get; set; }
        public string? ImagePath { get; set; }
        public bool? IsThumbnail { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
