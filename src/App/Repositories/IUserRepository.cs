using Yoli.Core.Domain.Entities;

namespace Yoli.Core.App.Repositories
{
    public interface IUserRepository
    {
        Task<IUser> GetUser(Func<IUser, bool> filter);
        Task<IUser> AddUser(IUser user);
    }
}