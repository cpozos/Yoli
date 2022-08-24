using WebApi.Providers;
using WebApi.Resolvers;

namespace Yoli.WebApi.Installers;

public static class ServiceCollectionExtensions
{
    public static void AddCustomProviders(this IServiceCollection services)
    {
        services.AddScoped<CustomSettingsProvider>(provider => id => GetCustomSettings(id, provider));
    }

    public static void AddResolvers(this IServiceCollection services)
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

    public static CustomSettings GetCustomSettings(int id, IServiceProvider provider)
    {
        return new CustomSettings(id.ToString());
    }
}
