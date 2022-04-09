using Yoli.Core.Domain.Entities;

namespace Yoli.Core.App.Services
{
    public interface ITokenService
    {
        Task<string> GenerateEmailConfirmationTokenAsync(string id);
    }
}