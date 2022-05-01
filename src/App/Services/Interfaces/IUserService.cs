using System.Security.Claims;
using Yoli.App.Dtos;
using Yoli.App.Entities;
using Yoli.Domain.Entities;

namespace Yoli.App.Services
{
    public interface IUserService
    {
        Task<Result<IUser>> AddUserAsync(PersonUserDto dto);
        Task<bool> UpdateUserAsync();

        // TODO: Replace adding Func<IUser,bool>
        Task<Result<IUser>> GetUserAsync();
        Task<Claim[]> GetClaims(int userId);
    }
}