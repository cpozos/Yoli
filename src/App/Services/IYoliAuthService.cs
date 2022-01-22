using Yoli.Core.App.Responses;
using Yoli.Core.Domain.Entities;

namespace Yoli.Core.App.Services
{
    public interface IYoliAuthService
    {
        Task<YoliAuthenticationResponse> GenerateAuthenticationResultForUserAsync(IUser user);
    }
}