using Yoli.Domain.Entities;
using Yoli.Domain.ValueObjects;

namespace Domain.Entities.Users
{
    public class AgencyIntegrant : Person
    {
        public AgencyRole Role { get; set; }
    }
}