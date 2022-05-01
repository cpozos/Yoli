namespace Yoli.WebApi.Installers
{
    public interface IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services);
    }
}