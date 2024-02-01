using eStore.Application.Common.Interfaces;
using System.Security.Claims;

namespace eStore.API.Services
{
    public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public long UserId => UserIdString();

        private long UserIdString()
        {
            _ = long.TryParse(
                _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier),
                out long userid
            );
            return userid;
        }
    }
}
