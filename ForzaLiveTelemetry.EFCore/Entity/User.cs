namespace ForzaLiveTelemetry.EFCore.Entity;
public class User
{
    public User()
    {
        LastSeen = DateTime.Now;
    }

    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string IPv4 { get; set; }
    public DateTime LastSeen { get; set; }
}
