namespace ForzaLiveTelemetry.Domain.DTO.User;
public class UserDto
{
    public UserDto(string userName, string pv4)
    {
        UserName = userName;
        IPv4 = pv4;
    }

    public string UserName { get; set; }
    public string IPv4 { get; set; }

}
