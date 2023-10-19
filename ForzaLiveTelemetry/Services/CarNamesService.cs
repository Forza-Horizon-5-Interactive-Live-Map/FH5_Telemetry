using ForzaLiveTelemetry.Domain.Model;
using ForzaLiveTelemetry.Domain.Setting;
using System.Net;

namespace ForzaLiveTelemetry.Services;

public class CarNamesService
{
    private readonly string _carNamesUrl;
    private readonly Dictionary<int, CarInfo> _carInfos = new();
    public CarNamesService(Settings settings)
    {
        _carNamesUrl = settings.CarNamesURL;
    }

    public async Task LoadInfos()
    {
        WebClient cli = new WebClient();
        string data = await cli.DownloadStringTaskAsync(_carNamesUrl);

        string[] lines = data.Split("\r\n");
        foreach (string line in lines.Skip(1))
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            string[] infos = line.Split('\t');
            CarInfo info = new CarInfo
            {
                Model = infos[0],
                Maker = infos[1],
                Year = int.Parse(infos[2]),
                Group = infos[3],
                CarOrdinal = !string.IsNullOrWhiteSpace(infos[4]) ? int.Parse(infos[4]) : -1,
                Weight = !string.IsNullOrWhiteSpace(infos[5]) ? int.Parse(infos[5]) : 0,
            };
            if (info.CarOrdinal != -1)
                _carInfos.Add(info.CarOrdinal, info);
        }
    }

    public CarInfo GetCarInfo(int carOrdinal)
    {
        if (_carInfos.TryGetValue(carOrdinal, out CarInfo? info))
            return info!;

        return new CarInfo()
        {
            Model = "Unknown",
            Maker = "Unknown",
            Year = 0,
            Group = "Unknown",
            CarOrdinal = 0,
            Weight = 0,
        };
    }
}
