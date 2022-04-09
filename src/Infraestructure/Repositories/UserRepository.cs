using Yoli.Core.App.Repositories;
using Yoli.Core.Domain.Entities;

namespace Yoli.Core.Infraestructure
{
    public class UserRepository : IUserRepository
    {
        private List<IUser> users = new List<IUser>
        {
            new User { Id = 1, Email = new Email("1@gmail.com"), Name = "1"},
            new Agency { Id = 2, Email = new Email("2@gmail.com"), Name = "2"},
        };

        public Task<IUser> AddUserAsync(IUser user)
        {
            user.Id = users.Count + 1;
            users.Add(user);
            return Task.FromResult(user);
        }

        public Task<IUser> GetUserAsync(Func<IUser, bool> filter)
        {
            var user = users.FirstOrDefault(filter);
            return Task.FromResult(user);
        }
    }
}