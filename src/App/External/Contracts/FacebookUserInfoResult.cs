using Newtonsoft.Json;

namespace App.External.Contracts
{
    public class FacebookUserInfoResult
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("data")]
        public FacebookPictureData Data { get; set; }
    }
}
