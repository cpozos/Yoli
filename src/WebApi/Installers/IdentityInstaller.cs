using Yoli.Core.App.Repositories;
using Yoli.Core.App.Services;
using Yoli.Core.Infraestructure;

namespace Yoli.Core.WebApi.Installers
{
    public class IdentityInstaller : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IYoliIdentityService, YoliIdentityService>();
            services.AddSingleton<IYoliAuthService, YoliAuthService>();
        }
    }
}