using System.Security.Claims;
using Yoli.Core.App.Dtos;
using Yoli.Core.App.Entities;
using Yoli.Core.App.Repositories;
using Yoli.Core.Domain.Aggregates;
using Yoli.Core.Domain.Entities;
using Yoli.Core.Domain.ValueObjects;

namespace Yoli.Core.App.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Result<IUser>> GetUserAsync()
        {
            var user = await _userRepository.GetUserAsync(u => u.Id == 1);
            var result = new Result<IUser>(user);
            return result;
        }
        public Task<Claim[]> GetClaims(int userId)
        {
            return Task.FromResult(new Claim[1] { new Claim(ClaimTypes.Name, "") });
        }

        public async Task<Result<IUser>> AddUserAsync(PersonUserDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                FirstName = dto.FirstName,
                SecondName = dto.SecondName,
                LastName = dto.LastName,
                Email = new EmailAddress(dto.Email),
                BirthDay = dto.BirthDay,
                ContactInformation = new PersonContactInfo()
            };

            await _userRepository.AddUserAsync(user);

            return new Result<IUser>(user);
        }

        public Task<bool> UpdateUserAsync()
        {
            throw new NotImplementedException();
        }
    }
}