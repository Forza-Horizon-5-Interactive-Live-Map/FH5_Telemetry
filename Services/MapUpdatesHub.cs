using Microsoft.AspNetCore.SignalR;

namespace ForzaDynamicMapApi.Services;

public class MapUpdatesHub : Hub
{
    private readonly ILogger _logger;

    public MapUpdatesHub(ILogger logger)
    {
        _logger = logger;
    }

    public override async Task OnConnectedAsync()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, "MapUpdates");
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _logger.LogInformation("Client disconnected. " + exception?.Message);
    }
}
