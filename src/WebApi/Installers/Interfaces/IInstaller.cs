namespace Yoli.WebApi.Installers.Interfaces;
public interface IInstaller
{
    public void InstallServices(IConfiguration configuration, IServiceCollection services);
}