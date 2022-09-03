using FluentValidation;
using Yoli.WebApi.Installers.Interfaces;
using Yoli.WebApi.Requests;
using Yoli.WebApi.Validations;

namespace Yoli.WebApi.Installers;

public class ValidatorsInstaller : IInstaller
{
    public void InstallServices(IConfiguration configuration, IServiceCollection services)
    {
        services.AddTransient<IValidator<YoliSignInRequest>, YoliSignInRequestValidator>();
        services.AddTransient<IValidator<PasswordChangeRequest>, PasswordChangeRequestValidator>();
        services.AddTransient<IYoliValidatorFactory, YoliValidatorFactory>();
    }
}