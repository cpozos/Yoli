using Yoli.Domain.ValueObjects;

namespace Yoli.Domain.Aggregates
{
    public class PersonContactInfo : GeneralContactInfo
    {
        public FacebookAccountInfo FacebookAccount { get; set; }
    }
}