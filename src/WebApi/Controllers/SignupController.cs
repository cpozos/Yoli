using Microsoft.AspNetCore.Mvc;
using Yoli.App.Services;
using Yoli.WebApi.Requests;
using Yoli.WebApi.Routes;
using Yoli.WebApi.Filters;
using Domain.ValueObjects;
using Yoli.App.Dtos;
using NETCore.MailKit.Core;

namespace Yoli.WebApi.Controllers
{
    [Route($"{ApiRoutes.Root}/{ApiVersion.V1}")]
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


        [HttpPost(ApiRoutes.IdentityRoutes.SignupYoli)]
        [RequestLogger]
        public async Task<IActionResult> SignUp([FromBody] YoliSignUpRequest request)
        {
            // Save data
            var user = new PersonUserDto
            {
                Name = $"{request.FirstName} {request.SecondName} {request.LastName}",
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

            var frontEndUrLink = 
                Url.Action(nameof(VerifyEmail), "Signup", new { token = token }, Request.Scheme, Request.Host.ToString());
            await _emailService.SendAsync("test@test.com", "Email verify", $"<a href=\"{frontEndUrLink}\">Click to verify</a>", true);

            return Ok();
        }

        [HttpGet(ApiRoutes.IdentityRoutes.VerifyEmail)]
        public async Task<IActionResult> VerifyEmail([FromQuery] VerifyEmailRequest request)
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
