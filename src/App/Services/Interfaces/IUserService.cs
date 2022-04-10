using System.Security.Claims;
using Yoli.Core.App.Dtos;
using Yoli.Core.App.Entities;
using Yoli.Core.Domain.Entities;

namespace Yoli.Core.App.Services
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