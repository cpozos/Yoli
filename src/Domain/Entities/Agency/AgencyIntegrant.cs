using Yoli.Domain.ValueObjects;

namespace Yoli.Domain.Entities;

public class AgencyIntegrant : Person
{
    public AgencyRole Role { get; set; }
}