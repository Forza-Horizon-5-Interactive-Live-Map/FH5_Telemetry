using ForzaLiveTelemetry.Domain.DTO.User;
using ForzaLiveTelemetry.Domain.Entity;
using ForzaLiveTelemetry.Domain.Mapper;
using ForzaLiveTelemetry.EFCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace ForzaLiveTelemetry.Services;

public class UserService
{
    private static DateTime _lastRealGet = DateTime.MinValue;
    private static ConcurrentDictionary<string, UserDto> players;

    public async Task<List<UserDto>> GetPlayerListAsync()
    {
        if (DateTime.Now - _lastRealGet > TimeSpan.FromSeconds(5))
        {
            using UserContext userContext = new();
            List<User> entities = await userContext.Users.ToListAsync();
            foreach (User entity in entities)
            {
                UserDto dto = entity.ToUserDto();
                players.AddOrUpdate(entity.IPv4, dto, (_, _) => dto);
            }
        }

        return players.Values.ToList();
    }
}
