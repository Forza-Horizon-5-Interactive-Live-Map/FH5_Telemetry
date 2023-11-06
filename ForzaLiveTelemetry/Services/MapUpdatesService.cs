using ForzaLiveTelemetry.Domain.DTO.Messages;
using ForzaLiveTelemetry.Domain.Mapper;
using ForzaLiveTelemetry.Domain.Model;
using ForzaLiveTelemetry.Domain.Setting;
using Microsoft.AspNetCore.SignalR;

namespace ForzaLiveTelemetry.Services;

public class MapUpdatesService : BackgroundService
{
    private readonly TimeSpan _period;
    private readonly MessagesService _messagesService;
    private readonly CarNamesService _carNamesService;
    private readonly IHubContext<MapUpdatesHub> _mapUpdatesHub;
    private readonly ILogger _logger;
    private int _executionCount = 0;
    public bool IsEnabled { get; set; } = true;

    public MapUpdatesService(Settings settings, MessagesService messagesService, CarNamesService carNamesService, IHubContext<MapUpdatesHub> mapUpdatesHub, ILogger logger)
    {
        _period = TimeSpan.FromMilliseconds(settings.MapUpdateMS);
        _messagesService = messagesService;
        _carNamesService = carNamesService;
        _mapUpdatesHub = mapUpdatesHub;

        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new PeriodicTimer(_period);
        while (
            !stoppingToken.IsCancellationRequested &&
            await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                if (IsEnabled)
                {
                    _messagesService.ClearOldMessages();

                    List<Message> messages = await _messagesService.GetMessagesAsync();
                    List<MessageDTO> messagesDTOs = messages.Select(m =>
                    {
                        CarInfo info = _carNamesService.GetCarInfo(m.CarOrdinal);
                        return m.ToDTO(info);
                    }).ToList();

                    await _mapUpdatesHub.Clients.All.SendAsync("MapUpdate", messagesDTOs);

                    _executionCount++;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to execute PeriodicHostedService with exception message : {Message}. Good luck next round!", ex.Message);
            }
        }
    }
}
