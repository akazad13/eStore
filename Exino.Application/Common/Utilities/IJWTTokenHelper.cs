using Exino.Domain.Entities;

namespace Exino.Application.Common.Utilities
{
    public interface IJWTTokenHelper
    {
        Task<string> GenerateJwtToken(AppUser user);
    }
}
