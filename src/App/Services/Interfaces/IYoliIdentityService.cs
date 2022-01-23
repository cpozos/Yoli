using Yoli.Core.App.Entities;

namespace Yoli.Core.App.Services
{
    public interface IYoliIdentityService
    {
        public Task<YoliIdentityResult> SigninUsingFacebookTask(string accessToken);
    }
}