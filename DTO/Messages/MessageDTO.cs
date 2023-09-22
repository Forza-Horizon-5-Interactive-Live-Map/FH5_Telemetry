using ForzaDynamicMapApi.Helper;

namespace ForzaDynamicMapApi.DTO.Messages;

public class MessageDTO
{
    public string Ip { get; set; }
    public string PlayerName { get; set; }
    public bool IsPaused { get; set; }
    public bool IsDisconnecting { get; set; }
    public float PosX { get; set; }
    public string PosXDisplay => DisplayMethods.PrintFloat(PosX, "00000.00", true);
    public float PosY { get; set; }
    public string PosYDisplay => DisplayMethods.PrintFloat(PosY, "00000.00", true);
    public float PosZ { get; set; }
    public string PosZDisplay => DisplayMethods.PrintFloat(PosZ, "00000.00", true);

    //speed
    public float Speed { get; set; }
    public float SpeedKmh => Speed * 3.6f;
    public string SpeedKmhDisplay => $"{DisplayMethods.PrintFloat(SpeedKmh, "0", false)} km/h";
    public float SpeedMph => Speed * 2.23694f;
    public string SpeedMphDisplay => $"{DisplayMethods.PrintFloat(SpeedMph, "0", false)} mph";

    //car
    public string Model { get; set; }
    public string Maker { get; set; }
    public int Year { get; set; }
    public string Group { get; set; }
    public int CarOrdinal { get; set; }
    public int Weight { get; set; }
}
