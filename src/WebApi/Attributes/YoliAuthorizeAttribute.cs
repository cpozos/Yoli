using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Yoli.App.Services;
using Yoli.Domain.Entities;

namespace Yoli.WebApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class YoliAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly JsonResult _unauthorizedResult = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        public YoliAuthorizeAttribute()
        {
        }

        public void OnAuthorization(AuthorizationFilterContext context)
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