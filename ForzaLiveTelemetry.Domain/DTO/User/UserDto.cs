namespace ForzaLiveTelemetry.Domain.DTO.User;
public class UserDto
{
    public UserDto(string userName, string ipv4)
    {
        UserName = userName;
        Ipv4 = ipv4;
    }

    public string UserName { get; set; }
    public string Ipv4 { get; set; }

}
