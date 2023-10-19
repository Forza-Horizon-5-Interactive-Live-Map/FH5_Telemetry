using ForzaLiveTelemetry.Domain.DTO.User;
using ForzaLiveTelemetry.Domain.Entity;
using ForzaLiveTelemetry.EFCore;
using ForzaLiveTelemetry.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ForzaLiveTelemetry.Controllers;

[Route("[controller]")]
[ApiController]
public class PlayersController : ControllerBase
{
    private readonly UserContext _context;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly PlayersService _playersStore;

    public PlayersController(UserContext context, UserManager<User> userManager, RoleManager<
        IdentityRole> roleManager, PlayersService playersStore)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        _playersStore = playersStore ?? throw new ArgumentNullException(nameof(playersStore));
    }

    //[AllowAnonymous]
    //[HttpPost]
    //[Route("Initialize")]
    //public async Task<IActionResult> Initialize()
    //{
    //    bool result = await DBInitializer.Initialize(_context, _userManager, _roleManager);
    //    string resultMessage = $"Initialisation DB : {(result ? "Succès" : "DB existe déja")}";

    //    return Ok(resultMessage);
    //}

    [HttpPost("")]
    public async Task<ActionResult> SetPlayerName([FromBody] SetUserNameDTO playerNameDTO)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        User isUserExist = await _userManager.FindByNameAsync(playerNameDTO.PlayerName);
        if (isUserExist is not null)
            return BadRequest("L'utilisateur existe déjà");

        User newUser = new()
        {
            UserName = playerNameDTO.PlayerName,
            IPv4 = playerNameDTO.PlayerIp
        };

        await _userManager.CreateAsync(newUser);
        return Ok();
    }

    [HttpGet("")]
    public async Task<ActionResult<User>> GetPlayerName([FromQuery] string playerName)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        User isUserExist = await _userManager.FindByNameAsync(playerName);
        if (isUserExist is not null)
            return BadRequest("L'utilisateur existe déjà");
        else
            return isUserExist;
    }
}
