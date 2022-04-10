using Yoli.Core.Domain.Entities;

namespace Yoli.Core.App.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(Func<User, bool> filter);
        Task<User> AddUserAsync(User user);
        Task<Agency> AddAgencyAsync(Agency agency);
    }
}