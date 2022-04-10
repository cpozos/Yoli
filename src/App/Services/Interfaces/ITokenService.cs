using Yoli.Core.Domain.Entities;

namespace Yoli.Core.App.Services
{
    public interface ITokenService
    {
        Task<string> GenerateEmailConfirmationTokenAsync(IUser user);
        Task<bool> ValidateEmailConfirmationTokenAsync(string token);
    }
}