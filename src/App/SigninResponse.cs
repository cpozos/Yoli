﻿using Yoli.Core.Domain.Entities;

namespace Yoli.Core.App
{
    public class SigninResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public SigninResponse(IUser user)
        {
            Id = user.Id;
            Name = user.UserName;
        }
    }
}