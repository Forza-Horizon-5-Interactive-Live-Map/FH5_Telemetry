using ForzaLiveTelemetry.Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ForzaLiveTelemetry.EFCore;
public class UserContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public UserContext() { }

    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>(u =>
        {
            u.HasKey(u => u.Id);

        });
    }

}
