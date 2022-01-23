using System.Text;
using Yoli.Core.App.Services;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    //private readonly AppSettings _appSettings;

    public JwtMiddleware(RequestDelegate next/*, IOptions<AppSettings> appSettings*/)
    {
        _next = next;
        //_appSettings = appSettings.Value;
    }

    public async Task Invoke(HttpContext context, IUserService userService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
            await AttachUserToContext(context, userService, token);

        await _next(context);
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