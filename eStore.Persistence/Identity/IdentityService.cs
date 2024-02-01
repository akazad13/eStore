using eStore.Application.Common.Interfaces;
using eStore.Application.Common.Wrappers;
using eStore.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eStore.Infrastructure.Identity
{
    public class IdentityService(
        UserManager<AppUser> userManager,
        IUserClaimsPrincipalFactory<AppUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService
        ) : IIdentityService
    {
        public async Task<AppUser?> AuthenticateUser(string email, string password)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return null;
            }

            var result = await userManager.CheckPasswordAsync(user, password);

            if (!result)
            {
                return null;
            }

            return user;
        }

        public async Task<string> GetUserNameAsync(long userId)
        {
            var user = await userManager.Users.FirstAsync(u => u.Id == userId);

            return user != null ? user.UserName?? "" : "";
        }

        public Task<IList<string>> GetUserRoles(AppUser user)
        {
            return userManager.GetRolesAsync(user);
        }

        public async Task<(Result Result, long UserId)> CreateUserAsync(
            string firstName,
            string lastName,
            string email,
            string password,
            bool isSubscribeToNewsletter,
            IEnumerable<string> assignedRoles
        )
        {
            var user = new AppUser
            {
                UserName = email,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                IsSubscribeToNewsletter = isSubscribeToNewsletter
            };

            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await userManager.AddToRolesAsync(user, assignedRoles);
            }

            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<bool> IsInRoleAsync(long userId, string role)
        {
            var user = userManager.Users.SingleOrDefault(u => u.Id == userId);

            return user != null && await userManager.IsInRoleAsync(user, role);
        }

        public async Task<bool> AuthorizeAsync(long userId, string policyName)
        {
            var user = userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return false;
            }

            var principal = await userClaimsPrincipalFactory.CreateAsync(user);

            var result = await authorizationService.AuthorizeAsync(principal, policyName);

            return result.Succeeded;
        }

        public async Task<Result> DeleteUserAsync(long userId)
        {
            var user = userManager.Users.SingleOrDefault(u => u.Id == userId);

            return user != null ? await DeleteUserAsync(user) : Result.Success();
        }

        public async Task<Result> DeleteUserAsync(AppUser user)
        {
            var result = await userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }

        public async Task<bool> IsUserExist(string? email)
        {
            if (email == null)
                return false;
            return await userManager.Users.AnyAsync(u => u.Email == email);
        }
    }
}
