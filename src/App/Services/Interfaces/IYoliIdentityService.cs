using Yoli.App.Entities;

namespace Yoli.App.Services
{
    public interface IYoliIdentityService
    {
        public Task<YoliIdentityResult> SigninUsingFacebookTask(string accessToken);
    }
}