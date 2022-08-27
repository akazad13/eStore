using Exino.Domain.Common;
using Exino.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exino.Domain.Entities
{
    public class AppUser: IdentityUser<long>
    {
        public string? ImagePath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<AppUserRole>? UserRoles { get; set; }


        public DateTimeOffset CreatedOn { get; set; }

        public long CreatedBy { get; set; }

        public DateTimeOffset? ModifiedOn { get; set; }

        public long? ModifiedBy { get; set; }
        public Status Status { get; set; }


        private readonly List<BaseEvent> _domainEvents = new();

        [NotMapped]
        public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(BaseEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(BaseEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
