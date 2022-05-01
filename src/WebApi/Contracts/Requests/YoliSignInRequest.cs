namespace Yoli.WebApi.Requests
{
    public record YoliSignInRequest : BaseRequest
    {
        public string SignInId { get; init; } = default!;
        public string Password { get; init; } = default!;
    }
}