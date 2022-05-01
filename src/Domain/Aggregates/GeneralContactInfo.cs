using Yoli.Domain.ValueObjects;

namespace Yoli.Domain.Aggregates
{
    public class GeneralContactInfo
    {
        public Location Location { get; set; }
        public IEnumerable<EmailAddress> Emails { get; set; }
        public IEnumerable<PhoneNumber> PhoneNumbers { get; set; }
    }
}
