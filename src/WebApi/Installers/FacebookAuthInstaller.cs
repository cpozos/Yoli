using Yoli.App.Options;
using Yoli.App.Services;
using Infraestructure.Services;
using Yoli.WebApi.Installers;

namespace WebApi.Installations
{
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
}