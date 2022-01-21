using App.External.Contracts;
using App.Options;
using App.Services;

namespace Infraestructure.Services
{
    internal class FacebookAuthService : IFacebookAuthService
    {
        private const string _tokenValidationUrl = "https://graph.facebook.com/debug_token?input_token={0}&access_token={1}|{2}";
        private const string _userInfoUrl = "https://graph.facebook.com/me?fields=first_name,last_name,picture&access_token={0}";
        private readonly FacebookAuthSettings _facebookAuthSettings;
        private readonly IHttpClientFactory _clientFactory;
        
        public Task<FacebookUserInfoResult> GetUserInfoAsync(string accessToken)
        {
            throw new NotImplementedException();
        }

        public Task<FacebookTokenValidationResult> ValidateAccessTokenAsync(string accessToken)
        {
            throw new NotImplementedException();
        }
    }
}