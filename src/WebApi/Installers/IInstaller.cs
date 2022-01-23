namespace Yoli.Core.WebApi.Installers
{
    public interface IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services);
    }
}