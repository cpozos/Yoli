using System.ComponentModel.DataAnnotations;

namespace Yoli.WebApi.Requests
{
    public record YoliSignUpRequest
    {
        [EmailAddress]
        public string Email { get; init; } = default!;
        public string FirstName { get; init; } = default!;
        public string SecondName { get; init; } = default!;
        public string LastName { get; init; } = default!;
        public DateTime BirthDay { get; init; } = default!;
        public string Password { get; init; } = default!;
    }
}