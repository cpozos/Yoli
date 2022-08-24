using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Yoli.Domain.Entities;
using Yoli.Shared.Constants;

namespace Yoli.WebApi.Authorization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class YoliAuthorizeAttribute : Attribute, IAuthorizationFilter, IAuthorizeData
{
    private readonly IList<string> _roles;
    public string? AuthenticationSchemes { get; set; }
    public string? Policy { get; set; }
    public string? Roles { get; set; }

    public YoliAuthorizeAttribute() : this(Array.Empty<string>(), null) { }
    public YoliAuthorizeAttribute(string[] roles) : this(roles, null) { }
    public YoliAuthorizeAttribute(string policy) : this(null, policy) { }
    public YoliAuthorizeAttribute(string[]? roles, string? policy)
    {
        _roles = roles ?? Array.Empty<string>();
        Roles = roles?.Length > 0 ? string.Join(",", roles) : null;
        Policy = policy;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // skip authorization if action is decorated with [AllowAnonymous] attribute
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;

        var user = context.HttpContext.Items[HttpContextItems.YoliUser] as IUser;
        if (user == null)
        {
            // It is not logged in
            context.Result = new UnauthorizedResult();
            return;
        }

        // TODO: Get roles for the user
        string userRole = "";

        if (_roles.Any() && !_roles.Contains(userRole))
        {
            context.Result = new ForbidResult();
            return;
        }
    }
}