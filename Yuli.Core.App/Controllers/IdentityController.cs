using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yoli.Core.App.Repositories;
using Yoli.Core.WebApi.Contracts;

namespace Yoli.Core.WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private ILogger _logger;
        private IUserRepository _userRepository;
        public IdentityController(ILogger<IdentityController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] YoliSignUpRequest request)
        {
            return Ok();
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] YoliBasicSigninRequest request)
        {
            if (request is null)
                return BadRequest();

            if (string.IsNullOrWhiteSpace(request.SignInId) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest();

            if (request.SignInId.Contains("@"))
            {
                // Get user by email
            }

            // Get user by UserId


            return Ok();
        }
    }
}
