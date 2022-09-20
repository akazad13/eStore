using Exino.Domain.Common;

namespace Exino.Domain.Entities
{
    public class Basket : BaseAuditableEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public AppUser? AppUser { get; set; }
        public decimal Subtotal { get; set; }
        public string? Email { get; set; }
        public ICollection<BasketItem>? BasketItems { get; set; }
    }
}
