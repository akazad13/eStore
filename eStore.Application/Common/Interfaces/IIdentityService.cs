﻿using eStore.Application.Common.Wrappers;
using eStore.Domain.Entities;

namespace eStore.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<AppUser?> AuthenticateUser(string email, string password);
        Task<AppUser> GetUserAsync(long userId);
        Task<bool> IsUserExist(string? email);
        Task<string> GetUserNameAsync(long userId);
        Task<IList<string>> GetUserRoles(AppUser user);
        Task<bool> IsInRoleAsync(long userId, string role);

        Task<bool> AuthorizeAsync(long userId, string policyName);

        Task<(Result Result, long UserId)> CreateUserAsync(
            string firstName,
            string lastName,
            string email,
            string password,
            bool isSubscribeToNewsletter,
            IEnumerable<string> assignedRoles
        );

        Task<Result> UpdateUserAsync(AppUser user);
        Task<Result> DeleteUserAsync(long userId);
    }
}
