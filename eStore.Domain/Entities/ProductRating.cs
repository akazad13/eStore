using eStore.Domain.Common;

namespace eStore.Domain.Entities
{
    public class ProductRating : BaseAuditableEntity
    {
        public long Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public long UserId { get; set; }
        public AppUser? AppUser { get; set; }
        public decimal Rating { get; set; }
    }
}
