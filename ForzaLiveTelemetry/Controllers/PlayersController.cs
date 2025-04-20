using ForzaLiveTelemetry.Domain.DTO.User;
using ForzaLiveTelemetry.Domain.Entity;
using ForzaLiveTelemetry.EFCore;
using ForzaLiveTelemetry.EFCore.IOC;
using ForzaLiveTelemetry.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForzaLiveTelemetry.Controllers;

[Route("[controller]")]
[ApiController]
public class PlayersController : ControllerBase
{
    private readonly UserContext _context;

    public PlayersController(UserContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpPost("")]
    public async Task<ActionResult> SetPlayerName([FromBody] SetUserNameDTO playerNameDTO)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (playerNameDTO.PlayerIp == "82.67.17.53")
            playerNameDTO.PlayerIp = "192.168.1.254";

        User? user = await _context.Users.FirstOrDefaultAsync(p => p.IPv4 == playerNameDTO.PlayerIp);
        if (user is not null)
        {
            user.UserName = playerNameDTO.PlayerName;
            _context.Users.Update(user);
        }
        else
        {
            User newUser = new()
            {
                UserName = playerNameDTO.PlayerName,
                IPv4 = playerNameDTO.PlayerIp
            };
            await _context.Users.AddAsync(newUser);
        }
            await _context.SaveChangesAsync();
        
            return Ok();
    }

    [HttpGet("CheckExistName")]
    public async Task<ActionResult<User>> GetPlayerName([FromQuery] string playerName)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        User? user = await _context.Users.FirstOrDefaultAsync(p => p.UserName == playerName);
        if (user is not null)
            return BadRequest("Le nom d'utilisateur existe déjà");
        else
            return user;
    }
}
