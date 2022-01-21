using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yoli.Core.WebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ILogger _logger;
        public UserController(ILogger<UserController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            return Ok($"Ok: {id}");
        }
    }
}