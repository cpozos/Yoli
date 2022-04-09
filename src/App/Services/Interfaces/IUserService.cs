using System.Security.Claims;
using Yoli.Core.App.Dtos;
using Yoli.Core.App.Entities;
using Yoli.Core.Domain.Entities;

namespace Yoli.Core.App.Services
{
    public interface IUserService
    {
        Task<bool> AddUserAsync(UserDto dto);
        Task<bool> UpdateUserAsync();
        Task<Result<IUser>> GetUserAsync();
        Task<Claim[]> GetClaims(int userId);
    }
}