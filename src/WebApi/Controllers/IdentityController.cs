using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yoli.App.Repositories;
using Yoli.App.Services;
using Yoli.Domain.Entities;
using Yoli.WebApi.Responses;
using Yoli.WebApi.Requests;
using Yoli.WebApi.Routes;
using Yoli.WebApi.Validations;

namespace Yoli.WebApi.Controllers;

[Route($"{ApiRoutes.Root}/{ApiVersion.V1}")]
[ApiController]
public class IdentityController : ControllerBase
{
    private IUserRepository _userRepository;
    private readonly IYoliIdentityService _yoliIdentityService;
    private readonly IYoliAuthService _yoliAuthService;
    private readonly IYoliValidatorFactory _validatorFactory;
    private readonly ITokenService _tokenService;

    public IdentityController(
        IUserRepository userRepository,
        IYoliIdentityService yoliIdentityService,
        IYoliAuthService yoliAuthService,
        IYoliValidatorFactory validatorFactory,
        ITokenService tokenService)
    {
        _userRepository = userRepository;
        _yoliIdentityService = yoliIdentityService;
        _yoliAuthService = yoliAuthService;
        _validatorFactory = validatorFactory;
        _tokenService = tokenService;
    }

    [HttpPost(ApiRoutes.IdentityRoutes.SigninFacebook)]
    public async Task<AuthenticationResponse> SignInFacebbok([FromBody] FacebookSignInRequest request)
    {
        var result = await _yoliIdentityService.SigninUsingFacebookTask(request.AccessToken);
        if (!result.Succeeded)
        {
            return new AuthenticationResponse(result.Errors);
        }

        var authResult = await _yoliAuthService.GenerateAuthenticationResultForUserAsync(result.User);
        return new AuthenticationResponse { Token = authResult.Token };
    }

    [HttpGet("face")]
    public async Task<IActionResult> ClientFacebookTest([FromQuery]string code)
    {
        var b = GeneratedIdentityApiRoutes.V1;
        var a = code?.ToString();

        return Ok(a);
    }

    [HttpPost("face")]
    public async Task<IActionResult> TokenFace([FromBody]object data)
    {
        return Ok(data);
    }

    

    [HttpPost(ApiRoutes.IdentityRoutes.SigninYoli)]
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
            if (!new EmailAddressAttribute().IsValid(request.SignInId))
            {
                return BadRequest();
            }
            user = await _userRepository.GetUserAsync(user => user.Email.Email == request.SignInId);
        }
        else
        {
            // Get user bu user name
            user = await _userRepository.GetUserAsync(user => user.Name == request.SignInId);
        }

        return user is null ? BadRequest() : Ok(new SigninResponse(user, _tokenService.GenerateToken(user)));
    }

    [AllowAnonymous]
    [HttpPost("fakeSignIn")]
    public async Task<IActionResult> FakeSignIn([FromBody] YoliSignInRequest request)
    {
        // Get user bu user name
        var user = await _userRepository.GetUserAsync(user => user.Name == "1");
        return user is null ? BadRequest() : Ok(new SigninResponse(user, _tokenService.GenerateToken(user)));
    }

    private async Task<bool> Validate<T>(T request)
    {
        var validator = _validatorFactory.GetValidator<T>();
        var result = await validator.ValidateAsync(request);
        return !result.IsValid;
    }
}
