using Microsoft.AspNetCore.Mvc;
using Yoli.Core.App.Repositories;
using Yoli.Core.Domain.Entities;
using Yoli.Core.App.Services;
using Yoli.Core.WebApi.Responses;
using Yoli.Core.WebApi.Requests;
using Yoli.Core.WebApi.Routes;
using Domain.ValueObjects;
using Yoli.Core.App.Dtos;

namespace Yoli.Core.WebApi.Controllers
{
    [Route(ApiRoutes.Root)]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private IUserService _userService;
        private readonly IYoliIdentityService _yoliIdentityService;
        private readonly IYoliAuthService _yoliAuthService;
        public  SignupController(
            IUserService userService,
            IYoliIdentityService yoliIdentityService,
            IYoliAuthService yoliAuthService)
        {
            _userService = userService;
            _yoliIdentityService = yoliIdentityService;
            _yoliAuthService = yoliAuthService;
        }


        [HttpPost($"{ApiVersion.V1}/{ApiRoutes.IdentityRoutes.SignupYoli}")]
        public async Task<IActionResult> SignUp([FromBody] YoliSignUpRequest request)
        {
            // Save data
            var user = new UserDto
            {
                FirstName = request.FirstName,
                SecondName = request.SecondName,
                LastName = request.LastName,
                Email = request.Email,
                BirthDay = new BirthDay(request.BirthDay)
            };

            var result = await _userService.AddUserAsync(user);
            if (!result)
                throw new Exception("An error occured");
            
            // Generate token for validation


            // Send email to validate

            return Ok(request);
        }

    }
}
