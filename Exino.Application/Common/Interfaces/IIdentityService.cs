using Exino.Application.Common.Wrappers;
using Exino.Domain.Entities;

namespace Exino.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<AppUser?> AuthenticateUser(string? email, string? password);
        Task<bool> IsUserExist(string? email);
        Task<string> GetUserNameAsync(long userId);
        Task<IList<string>> GetUserRoles(AppUser user);
        Task<bool> IsInRoleAsync(long userId, string role);

        Task<bool> AuthorizeAsync(long userId, string policyName);

        Task<(Result Result, long UserId)> CreateUserAsync(
            string? firstName,
            string? lastName,
            string? email,
            string? password,
            bool isSubscribeToNewsletter,
            IEnumerable<string> assignedRoles
        );

        Task<Result> DeleteUserAsync(long userId);
    }
}
