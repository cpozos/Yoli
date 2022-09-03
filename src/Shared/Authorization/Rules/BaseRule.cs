namespace Yoli.Shared.Authorization.Rules;

public abstract class BaseRule : IRule
{
    public IRule NextRule { get; private set; }

    public IRule SetNext(IRule rule)
    {
        NextRule = rule;
        return NextRule;
    }

    public async Task<bool> ValidateRule(RuleContext context)
    {
        bool result = await Validate(context);

        if (!result && NextRule != null)
        {
            return await NextRule.ValidateRule(context);
        }

        return result;
    }

    protected abstract Task<bool> Validate(RuleContext context);
}