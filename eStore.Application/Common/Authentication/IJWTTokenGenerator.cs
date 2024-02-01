using eStore.Domain.Entities;

namespace eStore.Application.Common.Authentication
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateJwtToken(AppUser user);
    }
}
