using System.Text.Json.Serialization;

namespace Yoli.App.External.Contracts;

public class FacebookTokenValidationData
{
    [JsonPropertyName("is_valid")]
    public bool IsValid { get; set; } = false;

    [JsonPropertyName("app_id")]
    public string AppId { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("application")]
    public string Application { get; set; } = string.Empty;

    [JsonPropertyName("data_access_expires_at")]
    public long DataAccessExpiresAt { get; set; }

    [JsonPropertyName("expires_at")]
    public long ExpiresAt { get; set; }

    [JsonPropertyName("scopes")]
    public string[] Scopes { get; set; } = Array.Empty<string>();

    [JsonPropertyName("user_id")]
    public string UserId { get; set; } = string.Empty;
}