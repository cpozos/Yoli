﻿using Yoli.Domain.ValueObjects;

namespace Yoli.Domain.Entities
{
    public interface IUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EmailAddress Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}