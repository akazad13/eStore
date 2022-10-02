using Exino.Domain.Common;

namespace Exino.Domain.Entities
{
    public class Address : BaseAuditableEntity
    {
        public long Id { get; set; }
        public string? Street { get; set; }

        public string? Street2 { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Country { get; set; }

        public string? Zip { get; set; }

        public long UserID { get; set; }
        public AppUser? AppUser { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }
}
