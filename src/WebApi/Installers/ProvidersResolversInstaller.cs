using Yoli.WebApi.Installers.Interfaces;
using Yoli.WebApi.Providers;
using Yoli.WebApi.Resolvers;
using Yoli.WebApi.Settings;

namespace Yoli.WebApi.Installers;

public class ProvidersResolversInstaller : IInstaller
{
    public void InstallServices(IConfiguration configuration, IServiceCollection services)
    {
        AddCustomProviders(services);
        AddResolvers(services);
    }

    private void AddCustomProviders(IServiceCollection services)
    {
        services.AddScoped<CustomSettingsProvider>(provider => id => GetCustomSettings(id, provider));
    }

    private void AddResolvers(IServiceCollection services)
    {
        services.AddScoped<CustomProcessor1>();
        services.AddScoped<CustomProcessor2>();

        services.AddScoped<CustomProcessorResolver>();
        services.AddScoped<CustomProcessorResolver2>(provider => processorName =>
        {
            var processor1 = provider.GetService<CustomProcessor1>();
            var processor2 = provider.GetService<CustomProcessor2>();

            ICustomProcessor? processor = processorName switch
            {
                "1" => processor1,
                "2" => processor2,
                _ => throw new ArgumentOutOfRangeException(nameof(processorName), ""),
            };

            return processor!;
        });
    }

    public CustomSettings GetCustomSettings(int id, IServiceProvider provider)
    {
        return new CustomSettings(id.ToString());
    }
}