using Microsoft.AspNetCore.Identity;

namespace ForzaLiveTelemetry.Domain.Entity;
public class User : IdentityUser<Guid>
{
    public User()
    {
        CreatedAt = DateTime.UtcNow;
        LastLogged = DateTime.UtcNow;
    }
    public string IPv4 { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastLogged { get; set; }
}
