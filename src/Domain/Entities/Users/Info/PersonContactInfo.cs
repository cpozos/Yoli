namespace Yoli.Core.Domain.Entities
{
    public class PersonContactInfo
    {
        public IEnumerable<PhoneNumber> PhoneNumbers { get; set; }
        public IEnumerable<string> Emails { get; set; }
        public FacebookAccountInfo FacebookAccount { get; set; }
    }
}