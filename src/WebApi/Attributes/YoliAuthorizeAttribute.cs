using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Yoli.Core.App.Services;
using Yoli.Core.Domain.Entities;

namespace Yoli.Core.WebApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class YoliAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly ITokenService _tokenService;
        private readonly JsonResult _unauthorizedResult = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        public YoliAuthorizeAttribute(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.Items["User"] as IUser;
            if (user == null)
            {
                // It is not logged in
                context.Result = _unauthorizedResult;
                return;
            }
        }
    }
}