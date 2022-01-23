using Yoli.Core.App.External.Contracts;

namespace Yoli.Core.App.Services
{
    public interface IFacebookAuthService
    {
        public Task<FacebookTokenValidationResult> ValidateAccessTokenAsync(string accessToken);
        public Task<FacebookUserInfoResult> GetUserInfoAsync(string accessToken);
    }
}
