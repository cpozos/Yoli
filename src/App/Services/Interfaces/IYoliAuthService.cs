using Yoli.App.Entities;
using Yoli.Domain.Entities;

namespace Yoli.App.Services
{
    public interface IYoliAuthService
    {
        Task<YoliAuthenticationResult> GenerateAuthenticationResultForUserAsync(IUser user);
    }
}