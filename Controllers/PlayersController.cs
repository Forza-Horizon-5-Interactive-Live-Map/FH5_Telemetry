using ForzaDynamicMapApi.DTO.Players;
using ForzaDynamicMapApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ForzaDynamicMapApi.Controllers;

[Route("[controller]")]
[ApiController]
public class PlayersController : ControllerBase
{
    [HttpPost("")]
    public async Task<ActionResult> SetPlayerName([FromServices] PlayersService playersStore, [FromBody] SetPlayerNameDTO playerNameDTO)
    {
        await playersStore.SetName(playerNameDTO.PlayerIp, playerNameDTO.PlayerName);
        return Ok();
    }
}
