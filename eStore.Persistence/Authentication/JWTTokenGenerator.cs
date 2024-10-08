﻿using eStore.Application.Common.Authentication;
using eStore.Application.Common.Interfaces;
using eStore.Application.Common.Utilities;
using eStore.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace eStore.Persistence.Authentication
{
    public class JwtTokenGenerator(
        IOptions<ConfigModel> configModel,
        IIdentityService identityService
        ) : IJwtTokenGenerator
    {
        private readonly ConfigModel _configModel = configModel.Value;

        public async Task<string> GenerateJwtToken(AppUser user)
        {
            var signingKey = Convert.FromBase64String(_configModel.Jwt.SigningSecret);
            var expiryDuration = _configModel.Jwt.ExpiryDuration;
            var validIssuer = _configModel.Jwt.ValidIssuer;
            var validAudience = _configModel.Jwt.ValidAudience;

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var userRoles = await identityService.GetUserRoles(user);

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var creds = new SigningCredentials(
                new SymmetricSecurityKey(signingKey),
                SecurityAlgorithms.HmacSha256Signature
            );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = validIssuer,
                Audience = validAudience,
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(expiryDuration),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = creds
            };

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var token = jwtTokenHandler.WriteToken(jwtToken);
            return token;
        }
    }
}
