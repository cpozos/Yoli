using Microsoft.AspNetCore.Authorization;
using Yoli.App.Authorization;
using Yoli.WebApi.Authorization;
using Yoli.WebApi.Installers.Interfaces;

namespace Yoli.WebApi.Installers;

public class AuthorizationInstaller : IInstaller
{
    public void InstallServices(IConfiguration configuration, IServiceCollection services)
    {
        services.AddScoped<IAuthorizationHandler, UserAccessHandler>();
        services.AddSingleton<IAuthorizationHandler, HasAccessHandler>();
        services.AddAuthorization(options =>
        {
            options.AddPolicy(YoliPolicy.MustHaveAccessPolicy, policy => policy.Requirements.Add(new HasAccessRequirement()));
            options.AddPolicy(YoliPolicy.UserAccessPolicy, policy => policy.Requirements.Add(new HasAccessRequirement()));
        });
    }
}