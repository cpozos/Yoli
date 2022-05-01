using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Yoli.Domain.Entities;
using Yoli.Shared.Constants;

namespace Yoli.Shared
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class YoliAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public YoliAuthorizeAttribute()
        {

        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.Items[HttpContextItems.YoliUser] as IUser;
            if (user == null)
            {
                // It is not logged in
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}