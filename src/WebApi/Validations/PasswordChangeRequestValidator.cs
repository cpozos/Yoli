using FluentValidation;
using WebApi.Contracts.Requests;

namespace Yoli.WebApi.Validations;

public class PasswordChangeRequestValidator : AbstractValidator<PasswordChangeRequest>
{
    public PasswordChangeRequestValidator()
    {
        RuleFor(c => c.Password)
            .NotEmpty()
            .ValidatePassword();
    }
}