using Exino.Domain.Entities;

namespace Exino.Application.Common.Authentication
{
    public interface IJWTTokenGenerator
    {
        Task<string> GenerateJwtToken(AppUser user);
    }
}
