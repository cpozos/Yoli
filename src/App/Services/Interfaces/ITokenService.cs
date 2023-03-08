using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Yoli.Domain.Entities;

namespace Yoli.App.Services;

public interface ITokenService
{
    Task<string> GenerateEmailConfirmationTokenAsync(IUser user);
    string GenerateToken(IUser user);
    bool ValidateToken(string token, out JwtSecurityToken jwtSecurityToken);
    bool GetClaimValue(JwtSecurityToken jwtSecurityToken, Func<Claim, bool> predicate, out string claimValue);
}