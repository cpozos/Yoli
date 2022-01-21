using App.External.Contracts;

namespace App.Services
{
    public interface IFacebookAuthService
    {
        public Task<FacebookTokenValidationResult> ValidateAccessTokenAsync(string accessToken);
        public Task<FacebookUserInfoResult> GetUserInfoAsync(string accessToken);
    }
}
