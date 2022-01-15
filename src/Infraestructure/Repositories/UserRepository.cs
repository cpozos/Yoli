using Yoli.Core.App.Repositories;
using Yoli.Core.Domain;

namespace Yoli.Core.Infraestructure
{
    public class UserRepository : IUserRepository
    {
        public async Task<User> GetUser(Func<User, bool> filter)
        {
            throw new NotImplementedException();
        }
    }
}