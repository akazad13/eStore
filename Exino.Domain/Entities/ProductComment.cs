using Exino.Domain.Common;

namespace Exino.Domain.Entities
{
    public class ProductComment : BaseAuditableEntity
    {
        public long Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public long UserId { get; set; }
        public AppUser? AppUser { get; set; }
        public string? Text { get; set; }
    }
}
