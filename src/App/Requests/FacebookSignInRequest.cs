namespace Yoli.Core.App.Requests
{
    public class FacebookSignInRequest
    {
        public string Email { get; init; } = default!;
        public string FirstName { get; init; } = default!;
        public string SecondName { get; init; } = default!;
        public string LastName { get; init; } = default!;
    }
}