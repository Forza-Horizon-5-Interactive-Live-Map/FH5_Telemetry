using ForzaDynamicMapApi.Models;
using System.Net;
using System.Net.Sockets;

namespace ForzaDynamicMapApi.Services;

public class TelemetryListener
{
    private readonly int _port;
    private readonly PlayersService _playerStore;
    private readonly MessagesService _messageStore;
    private readonly ILogger _logger;
    private readonly UdpClient _udpListener;
    private Task _listenerTask;
    private bool _isRunning;

    public TelemetryListener(Settings.Settings settings, PlayersService playerStore, MessagesService messageStore, ILogger logger)
    {
        _port = settings.Port;
        _playerStore = playerStore;
        _messageStore = messageStore;
        _logger = logger;

        _udpListener = new UdpClient(_port);
        //_udpListener.AllowNatTraversal(true);
        _isRunning = false;
    }

    public void StartListener()
    {
        _listenerTask = Task.Run(async () =>
        {
            var groupEndpoint = new IPEndPoint(IPAddress.Any, _port);
            _isRunning = true;
            try
            {
                while (_isRunning)
                {
                    byte[] bytes = _udpListener.Receive(ref groupEndpoint);
                    await SaveMessage(groupEndpoint.Address.ToString(), bytes);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());

            }
            finally
            {
                _udpListener.Close();
            }
        });
    }

    public void StopListener()
    {
        _isRunning = false;
    }

    private async Task SaveMessage(string playerIp, byte[] data)
    {
        var name = await _playerStore.GetName(playerIp);
        var message = new Message(playerIp, name, data);

        _messageStore.AddMessage(message);
    }
}
