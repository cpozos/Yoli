﻿using Domain.ValueObjects;

namespace Yoli.Core.Domain.Entities
{
    public class Person
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public BirthDay BirthDay { get; set; }
    }
}