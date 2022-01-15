using Yoli.Core.Domain;

namespace Yoli.Core.App.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUser(Func<User, bool> filter);
    }
}