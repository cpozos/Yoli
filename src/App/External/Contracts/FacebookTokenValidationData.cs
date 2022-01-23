using Newtonsoft.Json;

namespace Yoli.Core.App.External.Contracts
{
    public class FacebookTokenValidationData
    {
        [JsonProperty("is_valid")]
        public bool IsValid { get; set; } = false;

        [JsonProperty("app_id")]
        public string AppId { get; set; } = string.Empty;

        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;

        [JsonProperty("application")]
        public string Application { get; set; } = string.Empty;

        [JsonProperty("data_access_expires_at")]
        public long DataAccessExpiresAt { get; set; }

        [JsonProperty("expires_at")]
        public long ExpiresAt { get; set; }

        [JsonProperty("scopes")]
        public string[] Scopes { get; set; } = Array.Empty<string>();

        [JsonProperty("user_id")]
        public string UserId { get; set; } = string.Empty;
    }
}