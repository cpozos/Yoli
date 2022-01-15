using Yoli.Core.App.Repositories;
using Yoli.Core.Domain.Entities;

namespace Yoli.Core.Infraestructure
{
    public class UserRepository : IUserRepository
    {
        private IUser[] users = new IUser[] {
            new User { Id = 1, Email = new Email("1@gmail.com"), UserName = "1"},
            new Agency { Id = 2, Email = new Email("2@gmail.com"), UserName = "2"},
        };

        public Task<IUser> GetUser(Func<IUser, bool> filter)
        {
            var user = users.FirstOrDefault(filter);
            return Task.FromResult(user);
        }
    }
}