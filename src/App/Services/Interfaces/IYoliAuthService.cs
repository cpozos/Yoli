using Yoli.Core.App.Entities;
using Yoli.Core.Domain.Entities;

namespace Yoli.Core.App.Services
{
    public interface IYoliAuthService
    {
        Task<YoliAuthenticationResult> GenerateAuthenticationResultForUserAsync(IUser user);
    }
}