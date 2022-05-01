using Yoli.Domain.ValueObjects;

namespace Yoli.Domain.Entities
{
    public class Meeting
    {
        public Location Location { get; set; }
        public DateTime DateTime { get; set; }
    }
}
