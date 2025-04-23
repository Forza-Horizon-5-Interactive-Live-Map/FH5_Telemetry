using ForzaLiveTelemetry.Domain.DTO.User;
using ForzaLiveTelemetry.EFCore.Entity;

namespace ForzaLiveTelemetry.Domain.Mapper;
public static class UserMapper
{
    public static UserDto ToUserDto(this User user)
    {
        return new(user.UserName, user.IPv4);
    }
}
