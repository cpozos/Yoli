using Microsoft.AspNetCore.Authorization;

namespace Yoli.App.Authorization
{
    public class HasAccessRequirement : IAuthorizationRequirement
    {

    }

    public class HasAccessHandler : AuthorizationHandler<HasAccessRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasAccessRequirement requirement)
        {
            var user = context.User;
            return Task.CompletedTask;
        }
    }
}