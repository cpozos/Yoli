using Yoli.App.Options;
using Yoli.App.Services;
using Yoli.Infraestructure.Services;
using Yoli.WebApi.Installers.Interfaces;

namespace Yoli.WebApi.Installations;

public class FacebookAuthInstaller : IInstaller
{
    public void InstallServices(IConfiguration configuration, IServiceCollection services)
    {
        var facebookAuthSettings = new FacebookAuthSettings();
        configuration.Bind(nameof(FacebookAuthSettings), facebookAuthSettings);
        services.AddSingleton(facebookAuthSettings);

        services.AddHttpClient();
        services.AddSingleton<IFacebookAuthService, FacebookAuthService>();
    }
}