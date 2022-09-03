using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Yoli.App.Services;
using Yoli.Shared.Constants;

namespace Yoli.WebApi.Authentication;

public class YoliAuthenticationOptions : AuthenticationSchemeOptions
{

}
public class YoliAuthenticationHandler : AuthenticationHandler<YoliAuthenticationOptions>
{
    private readonly ITokenService _tokenService;

    public readonly IUserService _userService;

    public YoliAuthenticationHandler(IOptionsMonitor<YoliAuthenticationOptions> options,
        ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, ITokenService tokenService, IUserService userService)
        : base(options, logger, encoder, clock)
    {
        _tokenService = tokenService;
        _userService = userService;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        string? authHeaderValue = Request.Headers["Authorization"].FirstOrDefault();
        bool isValid = await ValidateHeaderAsync(authHeaderValue);
        if (isValid)
        {
            string? token = authHeaderValue!.Split(" ")[1];
            bool attachUserToContext =
                _tokenService.ValidateToken(token, out JwtSecurityToken jwt) &&
                _tokenService.GetClaimValue(jwt, x => x.Type == JwtRegisteredClaimNames.Sub, out string claimValue) &&
                int.TryParse(claimValue, out int userId);

            if (attachUserToContext)
            {
                // TODO : use userId
                var result = await _userService.GetUserAsync();
                if (result.Succeeded)
                {
                    // Attach user to context
                    Context.Items[HttpContextItems.YoliUser] = result.Data;
                    
                    // Generates the claims
                    var claims = new[] { new Claim("token", token) };
                    var identity = new ClaimsIdentity(claims, nameof(YoliAuthenticationHandler));
                    var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), Scheme.Name);
                    return AuthenticateResult.Success(ticket);
                }
            }
        }

        // do nothing if jwt validation fails
        // user is not attached to context so request won't have access to secure routes
        return AuthenticateResult.NoResult();
    }

    private async Task<bool> ValidateHeaderAsync(string? authHeaderValue)
    {
        var result = await Task.Run(() =>
        {
            if (string.IsNullOrWhiteSpace(authHeaderValue) || !authHeaderValue.StartsWith("Bearer"))
                return false;

            var words = authHeaderValue.Split(" ", StringSplitOptions.None);
            if (words.Length != 2)
                return false;

            return true;
        });

        return result;
    }
}