using Exino.Application.Common.Models;
using Exino.Domain.Entities;

namespace Exino.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<AppUser?> AuthenticateUser(string email, string password);
        Task<string> GetUserNameAsync(long userId);
        Task<IList<string>> GetUserRoles(AppUser user);
        Task<bool> IsInRoleAsync(long userId, string role);

        Task<bool> AuthorizeAsync(long userId, string policyName);

        Task<(Result Result, long UserId)> CreateUserAsync(string userName, string password);

        Task<Result> DeleteUserAsync(long userId);
    }
}
