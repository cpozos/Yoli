using Microsoft.AspNetCore.Mvc;
using Yoli.App.Repositories;
using Yoli.Domain.Entities;
using Yoli.App.Services;
using Yoli.WebApi.Requests;
using Yoli.WebApi.Routes;
using Domain.ValueObjects;
using Yoli.App.Dtos;

namespace Yoli.WebApi.Controllers
{
    [Route(ApiRoutes.Root)]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokeService;
        private readonly IEmailService _emailService;
        public  SignupController(
            IUserService userService,
            ITokenService tokenService,
            IEmailService emailService)
        {
            _userService = userService;
            _tokeService = tokenService;
            _emailService = emailService;
        }


        [HttpPost($"{ApiVersion.V1}/{ApiRoutes.IdentityRoutes.SignupYoli}")]
        public async Task<IActionResult> SignUp([FromBody] YoliSignUpRequest request)
        {
            // Save data
            var user = new PersonUserDto
            {
                FirstName = request.FirstName,
                SecondName = request.SecondName,
                LastName = request.LastName,
                Email = request.Email,
                BirthDay = new BirthDay(request.BirthDay)
            };

            var result = await _userService.AddUserAsync(user);
            if (!result.Succeeded)
                throw new Exception("An error occured");

            // Generate token for validation
            var token = await _tokeService.GenerateEmailConfirmationTokenAsync(result.Data);

            // Send email to validate
            var link = Url.Action(nameof(VerifyEmail), "Signup", new { userId = user.Id, code = token }, Request.Scheme, Request.Host.ToString());
            await _emailService.SendAsync("test@test.com", "Email verify", $"<a href=\"{link}\">Click to verify</a>", true);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> VerifyEmail([FromRoute] VerifyEmailRequest request)
        {
            var user = await _userService.GetUserAsync(); // TODO: GetUserAsync(u => u.Id == userId)

            // Do not return information about the error (hackers)
            if (user is null) 
                return BadRequest();

            // Confirm email in db
            //var result = await .ConfirmEmailAsync(user, code);
            //if (!result.Succeeded)
            //    return BadRequest();

            //TODO: Redirects to trusted signup page
            return Ok();
        }

    }
}
