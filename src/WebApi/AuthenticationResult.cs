using Microsoft.AspNetCore.Mvc;

namespace Yoli.Core.WebApi
{
    public class AuthenticationResponse : IActionResult
    {
        public bool IsValid => Errors.Count() > 0;
        public IEnumerable<string> Errors { get; set; } = Array.Empty<string>();
        public string Token { get; set; } = string.Empty;

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(IsValid ? Errors : Token)
            {
                StatusCode = Errors.Count() > 0
                ? StatusCodes.Status401Unauthorized
                : StatusCodes.Status200OK
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}