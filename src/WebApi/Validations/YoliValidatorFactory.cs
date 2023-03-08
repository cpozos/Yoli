using FluentValidation;
using Yoli.WebApi.Contracts.Requests;

namespace Yoli.WebApi.Validations;

public class YoliValidatorFactory : IYoliValidatorFactory
{
    private readonly Dictionary<Type, IValidator> validators = new();

    public YoliValidatorFactory(
        IValidator<YoliSignInRequest> validator,
        IValidator<PasswordChangeRequest> passwordValidator)
    {
        validators.Add(typeof(YoliSignInRequest), validator);
        validators.Add(typeof(PasswordChangeRequest), passwordValidator);
    }

    public IValidator<T> GetValidator<T>()
    {
        return validators.GetValueOrDefault(typeof(T)) as IValidator<T>;
    }

    public IValidator GetValidator(Type type)
    {
        return validators[type];
    }
}