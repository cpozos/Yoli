using Yoli.Core.App.Entities;
using Yoli.Core.Domain.Entities;

namespace Yoli.Core.App.Services
{
    public class YoliAuthService : IYoliAuthService
    {
        public async Task<YoliAuthenticationResult> GenerateAuthenticationResultForUserAsync(IUser user)
        {
            var response = new YoliAuthenticationResult(user.Id.ToString());
            return await Task.FromResult(response);
        }
    }
}