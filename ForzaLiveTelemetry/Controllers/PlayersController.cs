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
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly PlayersService _playersStore;

    public PlayersController(UserContext context, UserManager<User> userManager, RoleManager<
        IdentityRole<Guid>> roleManager, PlayersService playersStore)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        _playersStore = playersStore ?? throw new ArgumentNullException(nameof(playersStore));
    }

    [HttpPost]
    [Route("Initialize")]
    public async Task<IActionResult> Initialize()
    {
        bool result = await DBInitializer.Initialize(_context, _userManager, _roleManager);
        string resultMessage = $"Initialisation DB : {(result ? "Succès" : "DB existe déja")}";

        return Ok(resultMessage);
    }


    [HttpPost("")]
    public async Task<ActionResult> SetPlayerName([FromBody] SetUserNameDTO playerNameDTO)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (playerNameDTO.PlayerIp == "82.67.17.53")
            playerNameDTO.PlayerIp = "192.168.1.254";

        User? user = await _userManager.Users.FirstOrDefaultAsync(p => p.IPv4 == playerNameDTO.PlayerIp);
        IdentityResult res;
        if (user is not null)
        {
            user.UserName = playerNameDTO.PlayerName;
            res = await _userManager.UpdateAsync(user);
        }
        else
        {
            User newUser = new()
            {
                UserName = playerNameDTO.PlayerName,
                IPv4 = playerNameDTO.PlayerIp
            };
            res = await _userManager.CreateAsync(newUser);
        }

        if (res.Succeeded)
            return Ok();
        else
            return BadRequest(res.Errors);
    }

    [HttpGet("CheckExistName")]
    public async Task<ActionResult<User>> GetPlayerName([FromQuery] string playerName)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        User? user = await _userManager.FindByNameAsync(playerName);
        if (user is not null)
            return BadRequest("Le nom d'utilisateur existe déjà");
        else
            return user;
    }
}
