using Yoli.App.Repositories;
using Yoli.App.Services;
using Yoli.Infraestructure;

namespace Yoli.WebApi.Installers
{
    public class IdentityInstaller : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IYoliIdentityService, YoliIdentityService>();
            services.AddSingleton<IYoliAuthService, YoliAuthService>();
            services.AddSingleton<IUserService, UserService>();
        }
    }
}