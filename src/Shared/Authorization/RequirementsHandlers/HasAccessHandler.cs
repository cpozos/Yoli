using Microsoft.AspNetCore.Authorization;

namespace Shared.Authorization.RequirementsHandlers
{
    public class HasAccessRequirement : IAuthorizationRequirement
    {

    }

    public class HasAccessHandler : AuthorizationHandler<HasAccessRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasAccessRequirement requirement)
        {
            return Task.CompletedTask;
        }
    }
}