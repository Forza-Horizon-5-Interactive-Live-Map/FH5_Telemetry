namespace ForzaLiveTelemetry.Domain.Setting;
public class Roles
{
    public const string Admin = "Admin";
    public const string User = "User";
    public const string Unknown = "Unknown";

    public static string[] GetAllRoles() => new string[] { Admin, User, Unknown };
}
