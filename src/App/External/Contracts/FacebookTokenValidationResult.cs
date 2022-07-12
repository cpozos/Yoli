using System.Text.Json.Serialization;

namespace Yoli.App.External.Contracts;

public class FacebookTokenValidationResult
{
    [JsonPropertyName("data")]
    public FacebookTokenValidationData Data { get; set; } = new();
}