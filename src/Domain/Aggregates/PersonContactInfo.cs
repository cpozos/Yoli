using Yoli.Core.Domain.ValueObjects;

namespace Yoli.Core.Domain.Aggregates
{
    public class PersonContactInfo : GeneralContactInfo
    {
        public FacebookAccountInfo FacebookAccount { get; set; }
    }
}