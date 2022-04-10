using Yoli.Core.Domain.Aggregates;
using Yoli.Core.Domain.ValueObjects;

namespace Yoli.Core.Domain.Entities
{
    public class User : Person, IUser
    {
        public string Name { get; set; }
        public EmailAddress Email { get; set; }
        public PersonContactInfo ContactInformation { get; set; }
    }
}