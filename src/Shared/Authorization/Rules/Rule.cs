namespace Yoli.Shared.Authorization.Rules;

public interface IRule
{
    IRule NextRule { get; }
    IRule SetNext(IRule rule);
    Task<bool> ValidateRule(RuleContext context);
}