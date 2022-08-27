using FluentValidation;
using System.Text.RegularExpressions;

namespace Yoli.WebApi.Validations;

public static class PasswordPolicyValidator
{
    private static Regex regex = new Regex(@".*[a-z].*", RegexOptions.Compiled);
    public static IRuleBuilder<T, string> ValidatePassword<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Length(1, 100)
            .WithMessage("Length")
            .Must(password => !string.IsNullOrWhiteSpace(password) && regex.IsMatch(password))
            .WithMessage("Error");
    }
}