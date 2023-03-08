namespace Yoli.Domain.ValueObjects;

public class Location
{
    public string Name { get; set; }
    public Address Address { get; set; }
    public GpsInfo GpsInfo { get; set; }
}