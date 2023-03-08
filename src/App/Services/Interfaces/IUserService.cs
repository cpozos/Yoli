using System.Security.Claims;
using Yoli.App.Dtos;
using Yoli.Domain.Entities;

namespace Yoli.App.Services;

public interface IUserService
{
    Task<Result<IUser>> AddUserAsync(PersonUserDto dto);
    Task<bool> UpdateUserAsync();

    // TODO: Replace adding Func<IUser,bool>
    Task<Result<IUser>> GetUserAsync();
    Task<Result<IUser>> GetUserAsync(int id);
    Task<Claim[]> GetClaims(int userId);
}