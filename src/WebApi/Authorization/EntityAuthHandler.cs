using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Web;

namespace Yoli.WebApi.Authorization;
public abstract class EntityAccessHandler<T> : AuthorizationHandler<T> where  T : IAuthorizationRequirement
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    protected readonly string _entityFieldName;

    public EntityAccessHandler(IHttpContextAccessor httpContextAccessor, string entityFieldName)
    {
        _httpContextAccessor = httpContextAccessor;
        _entityFieldName = entityFieldName;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, T requirement)
    {
        if (context.User?.Identity?.IsAuthenticated != true)
        {
            context.Fail();
            return;
        }

        var routData = _httpContextAccessor.HttpContext.GetRouteData();
        var entityFieldValue = routData?.Values[_entityFieldName]?.ToString();

        if (string.IsNullOrWhiteSpace(entityFieldValue) && _httpContextAccessor.HttpContext.Request.QueryString.HasValue)
        {
            entityFieldValue = HttpUtility.ParseQueryString(_httpContextAccessor.HttpContext.Request.QueryString.Value)[_entityFieldName]?.ToString();
        }

        if (!Guid.TryParse(entityFieldValue, out Guid id))
        {
            // There is nothing to validate
            context.Succeed(requirement);
            return;
        }

        if (await UserCanAccessToEntity(id, context.User, context))
        {
            context.Succeed(requirement);
        }
    }

    protected abstract Task<bool> UserCanAccessToEntity(Guid entityId, ClaimsPrincipal userClaims, AuthorizationHandlerContext context = null);
}