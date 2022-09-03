using System.Security.Claims;

namespace Yoli.Shared.Authorization.Rules;

public class UserCanAccessUserRule : BaseRule
{
    public UserCanAccessUserRule()
    {
        
    }

    protected override Task<bool> Validate(RuleContext context)
    {
        var idClaim = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(idClaim?.Value, out Guid id))
        {
            return Task.FromResult(false);
        }

        bool userIsAccessingItsOwnData = context.EntityId == id;
        return Task.FromResult(userIsAccessingItsOwnData);
    }
}