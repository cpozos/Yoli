using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using Yoli.Core.App.Services;
using Yoli.Core.Domain.Entities;

namespace Yoli.Core.Infraestructure.Services
{
    public class TokenService : ITokenService
    {
        private const string _securedKey = "not_too_short_secret";
        private readonly string Issuer = "http://localhost:63154";
        private readonly string Audiance = "http://localhost:3000/";
        private readonly byte[] _keyBytes = Encoding.UTF8.GetBytes(_securedKey);

        public Task<string> GenerateEmailConfirmationTokenAsync(IUser user)
        {
            var token = GenerateToken(user);
            return Task.FromResult(token);
        }

        public bool GetClaimValue(JwtSecurityToken jwtSecurityToken, 
            Func<Claim, bool> predicate, out string claimValue)
        {
            claimValue = jwtSecurityToken.Claims.FirstOrDefault(predicate)?.Value ?? string.Empty;
            return !string.IsNullOrWhiteSpace(claimValue);
        }

        public bool ValidateToken(string token, out JwtSecurityToken jwtSecurityToken)
        {
            jwtSecurityToken = new JwtSecurityToken();

            if (string.IsNullOrWhiteSpace(token))
                return false;

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(_keyBytes),
                    ValidateAudience = true,
                    ValidAudience = Audiance,
                    ValidateIssuer = true,
                    ValidIssuer = Issuer,
                    ValidateLifetime = true
                }, out SecurityToken validatedToken);

                jwtSecurityToken = (JwtSecurityToken)validatedToken;
            }
            catch
            {
                return false;
            }

            return true;
        }

        private string GenerateToken(IUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email.Email),
                new Claim(JwtRegisteredClaimNames.Name, user.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            try
            {
                var securityKey = new SymmetricSecurityKey(_keyBytes);
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                   Issuer,
                   Audiance,
                   claims,
                   notBefore: DateTime.Now,
                   expires: DateTime.Now.AddHours(2),
                   credentials);

                var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);
                return tokenJson;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}