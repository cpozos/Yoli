using Yoli.WebApi.Installers.Interfaces;
using Yoli.App.Repositories;
using Yoli.App.Services;
using Yoli.Infraestructure;

namespace Yoli.WebApi.Installers;
public class IdentityInstaller : IInstaller
{
    public void InstallServices(IConfiguration configuration, IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IYoliIdentityService, YoliIdentityService>();
        services.AddScoped<IYoliAuthService, YoliAuthService>();
        services.AddScoped<IUserService, UserService>();
    }
}