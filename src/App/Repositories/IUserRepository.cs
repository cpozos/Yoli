using Yoli.Core.Domain.Entities;

namespace Yoli.Core.App.Repositories
{
    public interface IUserRepository
    {
        Task<IUser> GetUserAsync(Func<IUser, bool> filter);
        Task<IUser> AddUserAsync(IUser user);
    }
}