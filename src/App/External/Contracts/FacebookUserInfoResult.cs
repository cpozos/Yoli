using Newtonsoft.Json;

namespace Yoli.Core.App.External.Contracts
{
    public class FacebookUserInfoResult
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("email")]
        public string Email { get; set; } = string.Empty;

        [JsonProperty("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [JsonProperty("last_name")]
        public string LastName { get; set; } = string.Empty;

        [JsonProperty("data")]
        public FacebookPictureData Data { get; set; } = new();
    }
}
