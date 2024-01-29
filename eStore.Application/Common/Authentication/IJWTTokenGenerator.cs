using eStore.Domain.Entities;

namespace eStore.Application.Common.Authentication
{
    public interface IJWTTokenGenerator
    {
        Task<string> GenerateJwtToken(AppUser user);
    }
}
