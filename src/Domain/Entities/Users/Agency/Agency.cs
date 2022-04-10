using Yoli.Core.Domain.Aggregates;
using Yoli.Core.Domain.ValueObjects;

namespace Yoli.Core.Domain.Entities
{
    public class Agency : IUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EmailAddress Email { get; set; }
        public IEnumerable<Person> Integrants { get; set; }
        public AgencyContactInfo ContactInfo { get; set; }
        public IEnumerable<Agency> SubAgencies { get; set; }
    }
}