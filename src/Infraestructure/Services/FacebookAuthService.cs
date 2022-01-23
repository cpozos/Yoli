using Yoli.Core.App.External.Contracts;
using Yoli.Core.App.Options;
using Yoli.Core.App.Services;
using Newtonsoft.Json;

namespace Infraestructure.Services
{
    public class FacebookAuthService : IFacebookAuthService
    {
        private const string _tokenValidationUrl = "https://graph.facebook.com/debug_token?input_token={0}&access_token={1}|{2}";
        private const string _userInfoUrl = "https://graph.facebook.com/me?fields=first_name,last_name,picture&access_token={0}";
        private readonly FacebookAuthSettings _facebookAuthSettings;
        private readonly IHttpClientFactory _clientFactory;

        public FacebookAuthService(FacebookAuthSettings facebookAuthSettings, IHttpClientFactory clientFactory)
        {
            _facebookAuthSettings = facebookAuthSettings;
            _clientFactory = clientFactory;
        }

        public async Task<FacebookTokenValidationResult> ValidateAccessTokenAsync(string accessToken)
        {
            string formattedUrl = string.Format(_tokenValidationUrl, accessToken, _facebookAuthSettings.AppId, _facebookAuthSettings.AppSecret);
            var result = await _clientFactory.CreateClient().GetAsync(formattedUrl);
            result.EnsureSuccessStatusCode();
            var responseAsString = await result.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<FacebookTokenValidationResult>(responseAsString);

            if (json.Data.AppId != _facebookAuthSettings.AppId)
            {
                return new FacebookTokenValidationResult();
            }

            return json ?? new FacebookTokenValidationResult();
        }

        public async Task<FacebookUserInfoResult> GetUserInfoAsync(string accessToken)
        {
            string formattedUrl = string.Format(_userInfoUrl, accessToken);
            var result = await _clientFactory.CreateClient().GetAsync(formattedUrl);
            result.EnsureSuccessStatusCode();
            var responseAsString = await result.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<FacebookUserInfoResult>(responseAsString);
            return json ?? new FacebookUserInfoResult();
        }
    }
}