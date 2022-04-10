using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Yoli.Core.App.Services;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    //private readonly AppSettings _appSettings;

    public JwtMiddleware(RequestDelegate next /*, IOptions<AppSettings> appSettings*/)
    {
        _next = next;
        //_appSettings = appSettings.Value;
    }

    public async Task Invoke(HttpContext context, ITokenService tokenService, IUserService userService)
    {
        string? authHeaderValue = context.Request.Headers["Authorization"].FirstOrDefault();
        bool isValid = await ValidateHeaderAsync(authHeaderValue);
        if (isValid)
        {
            string? token = authHeaderValue!.Split(" ")[1];
            bool attachUserToContext =
                tokenService.ValidateToken(token, out JwtSecurityToken jwt) &&
                tokenService.GetClaimValue(jwt, x => x.Type == JwtRegisteredClaimNames.Sub, out string claimValue) &&
                int.TryParse(claimValue, out int userId);

            if (attachUserToContext)
            {
                // TODO : use userId
                var result = await userService.GetUserAsync();   
                if (result.Succeeded)
                {
                    context.Items["User"] = result.Data;
                }
            }
        }
       
        // do nothing if jwt validation fails
        // user is not attached to context so request won't have access to secure routes
        await _next(context);
    }


    private async Task<bool> ValidateHeaderAsync(string? authHeaderValue)
    {
        var result = await Task.Run(() =>
        {
            if (string.IsNullOrWhiteSpace(authHeaderValue) || !authHeaderValue.StartsWith("Bearer"))
                return false;

            var words = authHeaderValue.Split(" ", StringSplitOptions.None);
            if (words.Length != 1)
                return false;

            return true;
        });

        return result;
    }

    private async Task AttachUserToContext(HttpContext context, IUserService userService, string token)
    {
        try
        {
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            //tokenHandler.ValidateToken(token, new TokenValidationParameters
            //{
            //    ValidateIssuerSigningKey = true,
            //    IssuerSigningKey = new SymmetricSecurityKey(key),
            //    ValidateIssuer = false,
            //    ValidateAudience = false,
            //    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
            //    ClockSkew = TimeSpan.Zero
            //}, out SecurityToken validatedToken);

            //var jwtToken = (JwtSecurityToken)validatedToken;

            var userIdClaim = "1"; // jwtToken.Claims.First(x => x.Type == "id").Value

            var userId = int.Parse(userIdClaim);

            // attach user to context on successful jwt validation
            context.Items["User"] = await userService.GetUserAsync();
        }
        catch
        {
            // do nothing if jwt validation fails
            // user is not attached to context so request won't have access to secure routes
        }
    }
}