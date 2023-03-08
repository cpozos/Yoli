using Yoli.App.Entities;
using Yoli.Domain.Entities;

namespace Yoli.App.Services;

public class YoliAuthService : IYoliAuthService
{
    public async Task<YoliAuthenticationResult> GenerateAuthenticationResultForUserAsync(IUser user)
    {
        var response = new YoliAuthenticationResult(user.Id.ToString());
        return await Task.FromResult(response);
    }
}