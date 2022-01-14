using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yoli.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private ILogger _logger;
        public IdentityController(ILogger<IdentityController> logger)
        {
            _logger = logger;
        }
    }
}
