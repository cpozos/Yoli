using Yoli.Core.App.Services;
using Microsoft.AspNetCore.Mvc;
using Yoli.Core.WebApi.Routes;
using Yoli.Core.WebApi.Attributes;

namespace Yoli.Core.WebApi.Controllers
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