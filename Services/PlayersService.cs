using System.Collections.Concurrent;

namespace ForzaDynamicMapApi.Services;

public class PlayersService
{
    private readonly ConcurrentDictionary<string, string> players = new();


    public Task<string> GetName(string playerIp)
    {
        if (players.TryGetValue(playerIp, out var playerName))
            return Task.FromResult(playerName);
        else 
            return Task.FromResult("Unknown");
    }
    public Task SetName(string playerIp, string playerName)
    {
        players.AddOrUpdate(playerIp, playerName, (ip, name) => playerName);
        return Task.CompletedTask;
    }
}
