﻿using eStore.Domain.Common;
using eStore.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace eStore.Domain.Entities
{
    public class AppUser : IdentityUser<long>
    {
        public string? ImagePath { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool IsSubscribeToNewsletter { get; set; } = false;
        public DateTime? Birthdate { get; set; }
        public byte Gender { get; set; }
        public string? Locale { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public Status Status { get; set; }

        public ICollection<AppUserRole>? UserRoles { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<ProductComment>? ProductComments { get; set; }
        public ICollection<ProductRating>? ProductRatings { get; set; }
        public ICollection<Address>? Addresses { get; set; }
        public ICollection<Basket>? Baskets { get; set; }

        private readonly List<BaseEvent> _domainEvents = [];

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
