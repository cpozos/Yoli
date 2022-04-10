using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using Yoli.Core.Domain.Entities;

namespace Yoli.Core.App.Services
{
    public interface ITokenService
    {
        Task<string> GenerateEmailConfirmationTokenAsync(IUser user);
        bool ValidateToken(string token, out JwtSecurityToken jwtSecurityToken);
        bool GetClaimValue(JwtSecurityToken jwtSecurityToken, Func<Claim, bool> predicate, out string claimValue);
    }
}