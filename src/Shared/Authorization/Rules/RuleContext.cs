using System.Security.Claims;

namespace Yoli.Shared.Authorization.Rules;

public record RuleContext (Guid EntityId, string EntityName, ClaimsPrincipal User);