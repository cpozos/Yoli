using Microsoft.AspNetCore.Mvc;

namespace Yoli.Core.WebApi.Responses
{
    public class AuthenticationResponse : IActionResult
    {
        public bool IsValid { get; }
        public IEnumerable<string> Errors { get; }
        public string Token { get; init; } = string.Empty;

        public AuthenticationResponse(bool isValid = false, IEnumerable<string> errors = null)
        {
            IsValid = isValid;
            Errors = errors ?? Enumerable.Empty<string>();
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(IsValid ? Errors : Token)
            {
                StatusCode = IsValid
                ? StatusCodes.Status200OK
                : StatusCodes.Status401Unauthorized
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}