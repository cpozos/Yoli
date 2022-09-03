using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Yoli.Shared.Authorization.Rules;
using Yoli.WebApi.Contracts.RouteValues;

namespace Yoli.WebApi.Authorization;

public class UserAccessRequirement : IAuthorizationRequirement
{
    
}

public class UserAccessHandler : EntityAccessHandler<UserAccessRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserAccessHandler(IHttpContextAccessor httpContextAccessor)
        : base(httpContextAccessor, RouteEntities.UserEntity)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<bool> UserCanAccessToEntity(Guid entityId, ClaimsPrincipal userClaims, AuthorizationHandlerContext context = null)
    {
        IRule rule = new UserCanAccessUserRule(); // factory ??

        var result = await rule.ValidateRule(new RuleContext(entityId, _entityFieldName, userClaims));

        return result;
    }
}