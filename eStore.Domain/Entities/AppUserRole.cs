using Microsoft.AspNetCore.Identity;

namespace eStore.Domain.Entities
{
    public class AppUserRole : IdentityUserRole<long>
    {
        public AppUser? AppUser { get; set; }
        public Role? Role { get; set; }
    }
}
