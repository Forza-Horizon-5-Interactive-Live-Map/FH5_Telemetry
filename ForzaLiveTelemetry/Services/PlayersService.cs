using System.Collections.Concurrent;

namespace ForzaLiveTelemetry.Services;

public class PlayersService
{
    private readonly ConcurrentDictionary<string, string> players = new();

    public Task<string> GetName(string playerIp)
    {
        if (players.TryGetValue(playerIp, out string? playerName))
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
