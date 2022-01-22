using Microsoft.AspNetCore.Mvc;
using Yoli.Core.WebApi.Endpoints;
using Yoli.Core.App.Repositories;
using Yoli.Core.App.Requests;
using Yoli.Core.App.Responses;
using Yoli.Core.Domain.Entities;
using Yoli.Core.App.Services;

namespace Yoli.Core.WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private ILogger _logger;
        private IUserRepository _userRepository;
        private readonly IFacebookAuthService _facebookAuthService;
        private readonly IYoliAuthService _yoliAuthService;
        public IdentityController(ILogger<IdentityController> logger, 
            IUserRepository userRepository,
            IFacebookAuthService facebookAuthService,
            IYoliAuthService yoliAuthService)
        {
            _logger = logger;
            _userRepository = userRepository;
            _facebookAuthService = facebookAuthService;
            _yoliAuthService = yoliAuthService;
        }

        [HttpPost(IdentityEndpoint.SigninFacebook)]
        public async Task<AuthenticationResponse> SignInFacebbok([FromBody] FacebookSignInRequest request)
        {
            var result = await _facebookAuthService.ValidateAccessTokenAsync(request.AccessToken);

            if (!result.Data.IsValid)
            {
                return new AuthenticationResponse
                {
                    Errors = new[] { "Invalid Facebook Token" }
                };
            }

            var userInfo = await _facebookAuthService.GetUserInfoAsync(request.AccessToken);
            var user = await _userRepository.GetUser(u => u.Email.Address == userInfo.Email);
            if (userInfo is null)
            {
                var yoliUser = new User
                {
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    Email = new Email(userInfo.Email)
                };
                user = await _userRepository.AddUser(user);
            }

            //TODO: Merge both classes
            var authResult = await _yoliAuthService.GenerateAuthenticationResultForUserAsync(user);
            return new AuthenticationResponse { Token = authResult.Token };
        }

        [HttpGet("face")]
        public async Task<IActionResult> ClientFacebookTest([FromQuery]string code)
        {
            var a = code?.ToString();

            return Ok(a);
        }

        [HttpPost("face")]
        public async Task<IActionResult> TokenFace([FromBody]object data)
        {
            return Ok(data);
        }
        [HttpPost(IdentityEndpoint.SignupYoli)]
        public async Task<IActionResult> SignUp([FromBody] YoliSignUpRequest request)
        {
            return Ok(request);
        }

        [HttpPost(IdentityEndpoint.SigninYoli)]
        public async Task<IActionResult> SignIn([FromBody] YoliSignInRequest request)
        {
            if (request is null)
                return BadRequest();

            if (string.IsNullOrWhiteSpace(request.SignInId) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest();

            IUser user;
            if (request.SignInId.Contains("@"))
            {
                // Get user by email
                var email = new Email(request.SignInId);
                user = await _userRepository.GetUser(user => user.Email.Address == request.SignInId);
            }
            else
            {
                user = await _userRepository.GetUser(user => user.UserName == request.SignInId);
            }

            return user is null ? BadRequest() : Ok(new SigninResponse(user));
        }
    }
}
