using Yoli.Core.Domain.ValueObjects;

namespace Yoli.Core.Domain.Aggregates
{
    public class GeneralContactInfo
    {
        public Location Location { get; set; }
        public IEnumerable<EmailAddress> Emails { get; set; }
        public IEnumerable<PhoneNumber> PhoneNumbers { get; set; }
    }
}
