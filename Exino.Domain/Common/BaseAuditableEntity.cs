using Exino.Domain.Enums;

namespace Exino.Domain.Common
{
    public abstract class BaseAuditableEntity : BaseEntity
    {
        public DateTimeOffset CreatedOn { get; set; }

        public long CreatedBy { get; set; }

        public DateTimeOffset? ModifiedOn { get; set; }

        public long? ModifiedBy { get; set; }

        public Status Status { get; set; }
    }

}