using Yoli.App.External.Contracts;

namespace Yoli.App.Services;

public interface IFacebookAuthService
{
    public Task<FacebookTokenValidationResult> ValidateAccessTokenAsync(string accessToken);
    public Task<FacebookUserInfoResult> GetUserInfoAsync(string accessToken);
}