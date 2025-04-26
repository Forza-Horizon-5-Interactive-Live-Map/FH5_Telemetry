using ForzaLiveTelemetry.Domain.DTO.User;
using ForzaLiveTelemetry.Domain.Mapper;
using ForzaLiveTelemetry.EFCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using Bogus;
using ForzaLiveTelemetry.EFCore.Entity;

namespace ForzaLiveTelemetry.Services;

public class UserService
{
    private static DateTime _lastRealGet = DateTime.MinValue;
    private static ConcurrentDictionary<string, UserDto> players = new();
    private readonly IConfiguration _config;
    private readonly Faker _faker = new Faker();
    private readonly UserContext _userContext;
    public UserService(IConfiguration config, UserContext userContext)
    {
        _userContext = userContext;
        _config = config;
    }


    public async Task<List<UserDto>> GetPlayerListAsync()
    {
        if (DateTime.Now - _lastRealGet > TimeSpan.FromSeconds(5))
        {

            List<User> entities = await _userContext.Users.ToListAsync();

            players.Clear();
            foreach (User entity in entities)
            {
                UserDto dto = entity.ToUserDto();
                players.AddOrUpdate(entity.IPv4, dto, (_, _) => dto);
            }

            _lastRealGet =  DateTime.Now;
        }

        return players.Values.ToList();
    }
    
    public async Task AddPlayerOrUpdate(string playerIp)
    {

        User entity = _userContext.Users.FirstOrDefault(u => u.IPv4 == playerIp);
        if (entity is null)
        {
            entity = new User
            {
                IPv4 = playerIp,
                UserName = _faker.Internet.UserName(),
                LastSeen = DateTime.UtcNow,
            };
            await _userContext.Users.AddAsync(entity);
            players.TryAdd(playerIp, entity.ToUserDto());
        }
        else if (entity.LastSeen < DateTime.UtcNow.AddMonths(-1))
        {
            entity.LastSeen = DateTime.UtcNow;
        }
        
        await _userContext.SaveChangesAsync();
    }
    
}
