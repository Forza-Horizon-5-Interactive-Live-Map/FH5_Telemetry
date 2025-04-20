using ForzaLiveTelemetry.Domain.DTO.User;
using ForzaLiveTelemetry.Domain.Entity;
using ForzaLiveTelemetry.Domain.Mapper;
using ForzaLiveTelemetry.EFCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using Bogus;

namespace ForzaLiveTelemetry.Services;

public class UserService
{
    private static DateTime _lastRealGet = DateTime.MinValue;
    private static ConcurrentDictionary<string, UserDto> users = new();
    private static ConcurrentDictionary<string, UserDto> players = new();
    private readonly IConfiguration _config;
    private readonly Faker _faker = new Faker();
    public UserService(IConfiguration config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }

    private DbContextOptions<UserContext> GetOptions()
    {
        DbContextOptionsBuilder<UserContext> optionsBuilder = new DbContextOptionsBuilder<UserContext>();
        string? connectionString = _config.GetConnectionString("LiveMapSQL");

        if (connectionString == "DOCKER_CONNECTION_STRING")
            connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
        optionsBuilder.UseSqlServer(connectionString);

        return optionsBuilder.Options;
    }

    public async Task<List<UserDto>> GetPlayerListAsync()
    {
        if (DateTime.Now - _lastRealGet > TimeSpan.FromSeconds(5))
        {
            using UserContext userContext = new(GetOptions());

            List<User> entities = await userContext.Users.ToListAsync();

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

        using UserContext userContext = new(GetOptions());
        User entity = userContext.Users.FirstOrDefault(u => u.IPv4 == playerIp);
        if (entity is null)
        {
            entity = new User
            {
                IPv4 = playerIp,
                UserName = _faker.Internet.UserName(),
                LastSeen = DateTime.UtcNow,
            };
            await userContext.Users.AddAsync(entity);
            players.TryAdd(playerIp, entity.ToUserDto());
        }
        else if (entity.LastSeen < DateTime.UtcNow.AddMonths(-1))
        {
            entity.LastSeen = DateTime.UtcNow;
        }
        
        await userContext.SaveChangesAsync();
    }
    
}
