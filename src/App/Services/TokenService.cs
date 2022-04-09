using Yoli.Core.Domain.Entities;

namespace Yoli.Core.App.Services
{
    public class TokenService : ITokenService
    {
        public Task<string> GenerateEmailConfirmationTokenAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}