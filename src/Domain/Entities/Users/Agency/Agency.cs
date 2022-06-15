using Yoli.Domain.Aggregates;
using Yoli.Domain.ValueObjects;

namespace Yoli.Domain.Entities
{
    public class Agency : IUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EmailAddress Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public IEnumerable<Person> Integrants { get; set; }
        public AgencyContactInfo ContactInfo { get; set; }
        public IEnumerable<Agency> SubAgencies { get; set; }
    }
}