using Yoli.Core.App.Repositories;
using Yoli.Core.Domain;

namespace Yoli.Core.Infraestructure
{
    public class UserRepository : IUserRepository
    {
        private User[] users = new User[] {
            new User { Id = 1, Email = new Email("1@gmail.com"), UserName = "1"},
            new User { Id = 2, Email = new Email("2@gmail.com"), UserName = "2"},
        };

        public Task<User> GetUser(Func<User, bool> filter)
        {
            var user = users.FirstOrDefault(filter);
            return Task.FromResult(user);
        }
    }
}