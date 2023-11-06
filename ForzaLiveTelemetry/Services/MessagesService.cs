using ForzaLiveTelemetry.Domain.DTO.User;
using ForzaLiveTelemetry.Domain.Model;
using System.Collections.Concurrent;

namespace ForzaLiveTelemetry.Services;

public class MessagesService
{
    private static readonly ConcurrentDictionary<string, Message> lastMessages = new();
    private readonly UserService _userService;

    public MessagesService(UserService userService) => _userService=userService;

    public async Task<List<Message>> GetMessagesAsync()
    {
        List<UserDto> playerList = await _userService.GetPlayerListAsync();
        List<Message> messages = lastMessages.Values.ToList();

        foreach (Message message in messages)
        {
            UserDto? player = playerList.FirstOrDefault(p => p.IPv4 == message.Ip);
            if (player is not null)
                message.PlayerName = player.UserName;
        }
        return lastMessages.Values.ToList();
    }

    public void AddMessage(Message message)
    {
        lastMessages.AddOrUpdate(message.Ip, message, (ip, m) => message.IsRaceOn == 1 ? message : m);
        if (message.IsRaceOn == 0)
        {
            lastMessages[message.Ip].PlayerName = message.PlayerName;
            lastMessages[message.Ip].ReceivedTime = DateTime.UtcNow;
            lastMessages[message.Ip].IsRaceOn = 0;
        }
        ClearOldMessages();
    }
    public void ClearOldMessages()
    {
        foreach (KeyValuePair<string, Message> message in lastMessages.Where(m => DateTime.UtcNow - m.Value.ReceivedTime > TimeSpan.FromSeconds(10)))
        {
            lastMessages.TryRemove(message);
        }
    }
}
