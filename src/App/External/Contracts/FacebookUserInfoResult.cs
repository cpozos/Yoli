using System.Text.Json.Serialization;

namespace Yoli.App.External.Contracts;

public class FacebookUserInfoResult
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; } = string.Empty;

    [JsonPropertyName("last_name")]
    public string LastName { get; set; } = string.Empty;

    [JsonPropertyName("data")]
    public FacebookPictureData Data { get; set; } = new();
}