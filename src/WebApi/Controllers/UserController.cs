using Yoli.App.Services;
using Microsoft.AspNetCore.Mvc;
using Yoli.WebApi.Routes;
using Yoli.Shared;
using Yoli.Shared.Extensions;

namespace Yoli.WebApi.Controllers
{
    [Route(ApiRoutes.Base)]
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
            var user = _userService.GetUserAsync();
            return Ok(user);
        }
    }
}