namespace WebApi.Providers;

public delegate CustomSettings CustomSettingsProvider(int id);

public record CustomSettings(string Data);

