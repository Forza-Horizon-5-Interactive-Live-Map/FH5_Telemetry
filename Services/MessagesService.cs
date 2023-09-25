using ForzaDynamicMapApi.Models;
using System.Collections.Concurrent;

namespace ForzaDynamicMapApi.Services;

public class MessagesService
{
    private readonly ConcurrentDictionary<string, Message> lastMessages = new();
    public List<Message> GetMessages()
    {
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
        foreach (var message in lastMessages.Where(m => DateTime.UtcNow - m.Value.ReceivedTime > TimeSpan.FromSeconds(10)))
        {
            lastMessages.TryRemove(message);
        }
    }
}
