using FluentValidation;
using FluentValidation.TestHelper;
using Yoli.WebApi.Validations;
using Xunit;

namespace Yoli.WebApi.Tests;

public class YoliSignInRequestValidatorTest
{
    private readonly SubjectValidator _sut;

    public YoliSignInRequestValidatorTest()
    {
        _sut = new SubjectValidator();
    }

    [Theory]
    [InlineData("")]
    public void Validate_Should_Fail(string password)
    {
        var wrapper = new WrapperTest(password);
        var result = _sut.TestValidate(wrapper);
        result.ShouldHaveValidationErrorFor(w => w.Password);
    }

    [Theory]
    [InlineData("1a1")]
    public void Validate(string password)
    {
        var wrapper = new WrapperTest(password);
        var result = _sut.TestValidate(wrapper);
        result.ShouldNotHaveValidationErrorFor(w => w.Password);
    }
}

public record WrapperTest(string Password);
public class SubjectValidator : AbstractValidator<WrapperTest>
{
    public SubjectValidator()
    {
        RuleFor(t => t.Password)
            .ValidatePassword();
    }
}
