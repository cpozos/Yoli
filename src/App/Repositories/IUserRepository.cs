using Yoli.Domain.Entities;

namespace Yoli.App.Repositories;

public interface IUserRepository
{
    Task<User> GetUserAsync(Func<User, bool> filter);
    Task<User> AddUserAsync(User user);
    Task<Agency> AddAgencyAsync(Agency agency);
    Task<User> GetUser(int id);
}