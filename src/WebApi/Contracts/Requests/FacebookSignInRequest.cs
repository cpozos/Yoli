namespace Yoli.Core.WebApi.Requests
{
    public record FacebookSignInRequest
    {
        public string AccessToken { get; init; } = string.Empty!;
    }
}