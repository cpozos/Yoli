using DataAnnotationsExtensions;

namespace Yoli.Core.WebApi.Requests
{
    public record YoliSignUpRequest
    {
        [Email]
        public string Email { get; init; } = default!;
        public string FirstName { get; init; } = default!;
        public string SecondName { get; init; } = default!;
        public string LastName { get; init; } = default!;
        [Date]
        public DateTime BirthDay { get; init; } = default!;
        public string Password { get; init; } = default!;
    }
}