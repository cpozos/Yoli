using Newtonsoft.Json;

namespace Yoli.Core.App.External.Contracts
{
    public class FacebookTokenValidationResult
    {
        [JsonProperty("data")]
        public FacebookTokenValidationData Data { get; set; } = new();
    }
}