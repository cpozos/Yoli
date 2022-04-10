using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Yoli.Core.App.Services;
using Yoli.Core.Domain.Entities;

namespace Yoli.Core.WebApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class YoliAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly ITokenService _tokenService;
        private readonly JsonResult _unauthorizedResult = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        public YoliAuthorizeAttribute(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.Items["User"] as IUser;
            if (user == null)
            {
                // not logged in
                context.Result = _unauthorizedResult;
                return;
            }

            var auth = context.HttpContext.Items["Authorization"] as string;
            if (_tokenService.v)
            {
                context.Result = _unauthorizedResult;
            }

        }
    }
}