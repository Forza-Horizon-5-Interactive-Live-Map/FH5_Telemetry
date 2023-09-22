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
        lastMessages.AddOrUpdate(message.Ip, message, (ip, m) => message);

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
