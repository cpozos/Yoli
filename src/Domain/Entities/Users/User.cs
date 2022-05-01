using Yoli.Domain.Aggregates;
using Yoli.Domain.ValueObjects;

namespace Yoli.Domain.Entities
{
    public class User : Person, IUser
    {
        public string Name { get; set; }
        public EmailAddress Email { get; set; }
        public PersonContactInfo ContactInformation { get; set; }
    }
}