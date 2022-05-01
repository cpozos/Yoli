namespace Yoli.WebApi.Requests
{
    public record FacebookSignInRequest
    {
        public string AccessToken { get; init; } = string.Empty!;
    }
}