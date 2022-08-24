namespace Yoli.Shared.Authorization.Rules;

public interface IRule
{
    bool ValidateRule();
}

public class Rule : IRule
{
    public bool ValidateRule()
    {
        throw new NotImplementedException();
    }
}