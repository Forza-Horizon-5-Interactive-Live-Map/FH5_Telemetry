using ForzaDynamicMapApi.Services;
using ForzaLiveTelemety.DTO.Messages;
using ForzaLiveTelemety.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace ForzaDynamicMapApi.Controllers;

[Route("[controller]")]
[ApiController]
public class MessagesController : ControllerBase
{
    [HttpGet("")]
    public async Task<ActionResult<List<MessageDTO>>> GetMessages([FromServices] MessagesService messagesStore, [FromServices] CarNamesService carNamesService)
    {
        messagesStore.ClearOldMessages();

        var messages = messagesStore.GetMessages();
        return messages.Select(m =>
        {
            var info = carNamesService.GetCarInfo(m.CarOrdinal);
            return m.ToDTO(info);
        }).ToList();
    }
    [HttpGet("testbankal")]
    public async Task<IActionResult> getip()
    {
        return Ok($"local : {HttpContext.Connection.LocalIpAddress.ToString()}:{HttpContext.Connection.LocalPort.ToString()} \nRemote : {HttpContext.Connection.RemoteIpAddress.ToString()}:{HttpContext.Connection.RemotePort.ToString()}");
    }
}
