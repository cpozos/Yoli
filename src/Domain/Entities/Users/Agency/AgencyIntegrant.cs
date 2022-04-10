using Yoli.Core.Domain.Entities;
using Yoli.Core.Domain.ValueObjects;

namespace Domain.Entities.Users
{
    public class AgencyIntegrant : Person
    {
        public AgencyRole Role { get; set; }
    }
}