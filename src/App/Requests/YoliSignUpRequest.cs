namespace Yoli.Core.App.Requests
{
    public record YoliSignUpRequest
    {
        public string Email { get; init; } = default!;
        public string FirstName { get; init; } = default!;
        public string SecondName { get; init; } = default!;
        public string LastName { get; init; } = default!;
        public DateTime BirthDay { get; init; } = default!;
    }
}