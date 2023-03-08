using Microsoft.AspNetCore.Mvc;

namespace Yoli.WebApi.Contracts.Responses;

public class AuthenticationResponse : IActionResult
{
    public bool IsValid => !Errors.Any() && !string.IsNullOrEmpty(Token);
    public IEnumerable<string> Errors { get; }
    public string Token { get; init; } = string.Empty;

    public AuthenticationResponse(IEnumerable<string> errors = null)
    {
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