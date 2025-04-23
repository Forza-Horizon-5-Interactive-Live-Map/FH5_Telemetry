using ForzaLiveTelemetry.EFCore.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ForzaLiveTelemetry.EFCore;
public class UserContext : DbContext
{
    
    public DbSet<User> Users { get; set; }
    
    public UserContext() { }

    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>(u =>
        {
            u.ToTable("users");
            
            u.HasKey(u => u.Id);
            u.Property(u => u.UserName).IsRequired().HasMaxLength(50);
            u.Property(u => u.IPv4).IsRequired(true);
            u.Property(u => u.LastSeen).IsRequired().HasDefaultValue(DateTime.Now);
        });
    }

}
