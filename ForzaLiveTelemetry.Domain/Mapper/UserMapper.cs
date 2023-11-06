using ForzaLiveTelemetry.Domain.DTO.User;
using ForzaLiveTelemetry.Domain.Entity;

namespace ForzaLiveTelemetry.Domain.Mapper;
public static class UserMapper
{
    public static UserDto ToUserDto(this User user)
    {
        return new()
        {
            UserName = user.UserName,
            IPv4 = user.IPv4
        };
    }
}
