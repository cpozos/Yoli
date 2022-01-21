using Newtonsoft.Json;

namespace App.External.Contracts
{
    public class FacebookTokenValidationResult
    {
        [JsonProperty("data")]
        public FacebookTokenValidationData Data { get; set; }
    }
}