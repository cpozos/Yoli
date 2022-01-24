﻿using Yoli.Core.App.Entities;
using Yoli.Core.App.Repositories;
using Yoli.Core.Domain.Entities;

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
            var result = new Result<IUser> { Data = user };
            return result;
        }
    }
}