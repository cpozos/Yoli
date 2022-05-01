using Yoli.App.Entities;
using Yoli.App.Repositories;
using Yoli.Domain.Entities;
using Yoli.Domain.ValueObjects;

namespace Yoli.App.Services
{
    public class YoliIdentityService : IYoliIdentityService
    {
        private readonly IFacebookAuthService _facebookAuthService;
        private readonly IUserRepository _userRepository;

        public YoliIdentityService(IUserRepository userRepository, IFacebookAuthService facebookAuthService)
        {
            _userRepository = userRepository;
            _facebookAuthService = facebookAuthService;
        }

        public async Task<YoliIdentityResult> SigninUsingFacebookTask(string accessToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                return new YoliIdentityResult(new[] { "Invalid Facebook Token" });
            }

            var result = (await _facebookAuthService.ValidateAccessTokenAsync(accessToken)).Data;
            if (!result.IsValid)
            {
                return new YoliIdentityResult(new[] { "Invalid Facebook Token" });
            }

            var facebookUserInfo = await _facebookAuthService.GetUserInfoAsync(accessToken);
            var user = await _userRepository.GetUserAsync(u => u.Email.Email == facebookUserInfo.Email);
            if (user is null)
            {
                user = new User
                {
                    FirstName = facebookUserInfo.FirstName,
                    LastName = facebookUserInfo.LastName,
                    Email = new EmailAddress(facebookUserInfo.Email, true)
                };
                user = await _userRepository.AddUserAsync(user);
            }

            ArgumentNullException.ThrowIfNull(user);
            return new YoliIdentityResult { User = user };
        }

        
    }
}