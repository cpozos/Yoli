﻿using Yoli.App.Repositories;
using Yoli.Domain.Entities;
using Yoli.Domain.ValueObjects;
using Yoli.Infraestructure.Persistance;

namespace Yoli.Infraestructure;

public class UserRepository : IUserRepository
{
    private List<User> users = new List<User>
    {
        new User { Id = 1, Email = new EmailAddress("1@gmail.com", true), Name = "1"},

    };

    private List<Agency> agencies = new List<Agency>
    {
        new Agency { Id = 2, Name = "2"},
    };

    private readonly YoliDbContext _context;


    public UserRepository(YoliDbContext context)
    {
        _context = context;
        _context.Database.EnsureCreated();
    }

    public Task<Agency> AddAgencyAsync(Agency agency)
    {
        agency.Id = agencies.Count + 1;
        agencies.Add(agency);
        return Task.FromResult(agency);
    }

    public Task<User> AddUserAsync(User user)
    {
        user.Id = users.Count + 1;
        users.Add(user);
        return Task.FromResult(user);
    }

    public Task<User> GetUserAsync(Func<User, bool> filter)
    {
        var user = users.FirstOrDefault(filter);
        return Task.FromResult(user);
    }

    public async Task<User> GetUser(int id)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        if (user is null)
            return null;

        var userDomain = new User()
        {
            Id = user.Id
        };
        return userDomain;
    }
}