namespace Yoli.WebApi.Contracts.Requests;

public record FacebookSignInRequest
{
    public string AccessToken { get; init; } = string.Empty!;
}