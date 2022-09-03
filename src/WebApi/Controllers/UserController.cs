using Microsoft.AspNetCore.Mvc;
using Yoli.App.Authorization;
using Yoli.App.Services;
using Yoli.WebApi.Routes;
using Yoli.WebApi.Authorization;
using Microsoft.AspNetCore.Authorization;
using WebApi.Extensions;

namespace Yoli.WebApi.Controllers;

[Route($"{ApiRoutes.Root}/{ApiVersion.V1}")]
[ApiController]
public class UserController : ControllerBase
{
    private IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [YoliAuthorize]
    [HttpGet("users/{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var currentUser = this.GetCurrentUser();
        var user = _userService.GetUserAsync();
        return Ok(user);
    }

    [HttpGet("users2/{id}")]
    public async Task<IActionResult> GetUser2(int id)
    {
        //var currentUser = this.GetCurrentUser();
        var user = await _userService.GetUserAsync(id);
        return Ok(user);
    }

    [HttpGet("users/policy")]
    [Authorize(YoliPolicy.MustHaveAccessPolicy)]
    public async Task<IActionResult> TestPolicy()
    {
        return Ok("Yes");
    }
}