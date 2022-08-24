using FluentValidation;
using Yoli.WebApi.Requests;

namespace Yoli.WebApi.Validations
{
    public class YoliValidatorFactory : IYoliValidatorFactory
    {
        private readonly Dictionary<Type, IValidator> validators = new();

        public YoliValidatorFactory(IValidator<YoliSignInRequest> validator)
        {
            validators.Add(typeof(YoliSignInRequest), validator);
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
}