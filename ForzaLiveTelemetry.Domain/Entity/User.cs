using Microsoft.AspNetCore.Identity;

namespace ForzaLiveTelemetry.Domain.Entity;
public class User
{
    public User()
    {
        LastSeen = DateTime.UtcNow;
    }

    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string IPv4 { get; set; }
    public DateTime LastSeen { get; set; }
}
