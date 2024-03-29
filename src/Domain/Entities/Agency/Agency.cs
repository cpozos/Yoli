﻿using Yoli.Domain.Aggregates;

namespace Yoli.Domain.Entities;

public class Agency
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<Person> Integrants { get; set; }
    public AgencyContactInfo ContactInfo { get; set; }
    public IEnumerable<Agency> SubAgencies { get; set; }
}