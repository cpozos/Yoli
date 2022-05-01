using Newtonsoft.Json;

namespace Yoli.App.External.Contracts
{
    public class FacebookTokenValidationResult
    {
        [JsonProperty("data")]
        public FacebookTokenValidationData Data { get; set; } = new();
    }
}