using Exino.Application.Common.Interfaces;
using Exino.Application.Common.Wrappers;
using Exino.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Exino.Infrastructure.Identity
{


    public class IdentityService : IIdentityService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<AppUser> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;

        public IdentityService(
            UserManager<AppUser> userManager,
            IUserClaimsPrincipalFactory<AppUser> userClaimsPrincipalFactory,
            IAuthorizationService authorizationService)
        {
            _userManager = userManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
        }
        public async Task<AppUser?> AuthenticateUser(string email, string password)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return null;
            }

            var result = await _userManager.CheckPasswordAsync(user, password);

            if (!result)
            {
                return null;
            }

            return user;
        }
        public async Task<string> GetUserNameAsync(long userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }
        public Task<IList<string>> GetUserRoles(AppUser user)
        {
            return _userManager.GetRolesAsync(user);
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

            var result = await _userManager.CreateAsync(user, password);
            
            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, assignedRoles);
            }

            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<bool> IsInRoleAsync(long userId, string role)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return user != null && await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<bool> AuthorizeAsync(long userId, string policyName)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return false;
            }

            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

            var result = await _authorizationService.AuthorizeAsync(principal, policyName);

            return result.Succeeded;
        }

        public async Task<Result> DeleteUserAsync(long userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return user != null ? await DeleteUserAsync(user) : Result.Success();
        }

        public async Task<Result> DeleteUserAsync(AppUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }

        public async Task<bool> IsUserExist(string email)
        {
            if (email == null)
                return false;
            return await _userManager.Users.AnyAsync(u => u.Email == email);
        }
    }

}