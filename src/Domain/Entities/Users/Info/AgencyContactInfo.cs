namespace Yoli.Core.Domain.Entities
{
    public class AgencyContactInfo
    {
        public Location Location { get; set; }
        public IEnumerable<string> Emails { get; set; }
        public IEnumerable<PhoneNumber> PhoneNumbers { get; set; }
    }
}