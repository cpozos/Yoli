namespace Yoli.Core.App.Requests
{
    public record FacebookSignInRequest
    {
        public string AccessToken { get; init; } = default!;
    }
}