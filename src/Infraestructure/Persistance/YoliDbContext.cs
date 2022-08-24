using Microsoft.EntityFrameworkCore;
using Domain.Persistance.Entities;

namespace Yoli.Infraestructure.Persistance;

public class YoliDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public YoliDbContext(DbContextOptions<YoliDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = 1,
            Email = "",
            Name = "",
            Password = "",
            Salt = ""
        });
    }
}
