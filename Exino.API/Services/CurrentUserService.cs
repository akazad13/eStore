using Exino.Application.Common.Interfaces;
using System.Security.Claims;

namespace Exino.API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public long UserId => UserIdString();

        private long UserIdString()
        {
            _ = long.TryParse(_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier), out long userid);
            return userid;

        }
    }
}
