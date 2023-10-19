using ForzaLiveTelemetry.Domain.DTO.Messages;
using ForzaLiveTelemetry.Domain.Mapper;
using ForzaLiveTelemetry.Domain.Model;
using ForzaLiveTelemetry.Services;
using Microsoft.AspNetCore.Mvc;

namespace ForzaLiveTelemetry.Controllers;

[Route("[controller]")]
[ApiController]
public class MessagesController : ControllerBase
{
    private readonly MessagesService _messagesStore;

    public MessagesController(MessagesService messagesStore)
    {
        _messagesStore = messagesStore ?? throw new ArgumentNullException(nameof(messagesStore));
    }

    [HttpGet("")]
    public async Task<ActionResult<List<MessageDTO>>> GetMessages([FromServices] CarNamesService carNamesService)
    {
        _messagesStore.ClearOldMessages();

        List<Message> messages = await _messagesStore.GetMessagesAsync();
        return messages.Select(m =>
        {
            CarInfo info = carNamesService.GetCarInfo(m.CarOrdinal);
            return m.ToDTO(info);
        }).ToList();
    }
}
