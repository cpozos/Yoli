using FluentValidation;

namespace Yoli.WebApi.Validations;

public interface IYoliValidatorFactory
{
    public IValidator<T> GetValidator<T>();
}