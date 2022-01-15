﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yoli.Core.App.Repositories;
using Yoli.Core.App.Requests;
using Yoli.Core.App.Responses;
using Yoli.Core.Domain.Entities;

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
            return Ok(request);
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] FacebookSignInRequest request)
        {
            return Ok(request);
        }

        [HttpPost("signin")]
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
