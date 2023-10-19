using ForzaLiveTelemetry.Domain.Helper;

namespace ForzaLiveTelemetry.Domain.DTO.Messages;

public class MessageDTO
{
    public string Id { get; set; }
    public string PlayerName { get; set; }
    public bool IsPaused { get; set; }
    public bool IsDisconnecting { get; set; }
    public float PosX { get; set; }
    public string PosXDisplay => DisplayMethods.PrintFloat(PosX, "00000.00", true);
    public float PosY { get; set; }
    public string PosYDisplay => DisplayMethods.PrintFloat(PosY, "00000.00", true);
    public float PosZ { get; set; }
    public string PosZDisplay => DisplayMethods.PrintFloat(PosZ, "00000.00", true);

    public float GAcceleration { get; set; }
    public string GAccelerationDisplay => $"{DisplayMethods.PrintFloat(GAcceleration, "0.0", false)} G";

    public float Yaw { get; set; }
    public float Pitch { get; set; }
    public float Roll { get; set; }

    // computed position for map
    public float Lat => Constantes.LocalisationMapCenterLat + PosZ / Constantes.LocalisationRatio;
    public float Lng => Constantes.LocalisationMapCenterLng + PosX / Constantes.LocalisationRatio;

    // car data
    public float Speed { get; set; }
    public float SpeedKmh => Speed * 3.6f;
    public string SpeedKmhDisplay => $"{DisplayMethods.PrintFloat(SpeedKmh, "0", false)} kmh";
    public float SpeedMph => Speed * 2.23694f;
    public string SpeedMphDisplay => $"{DisplayMethods.PrintFloat(SpeedMph, "0", false)} mph";
    public float Power { get; set; }
    public float PowerKw => Power / 1000;
    public string PowerKwDisplay => $"{DisplayMethods.PrintFloat(PowerKw, "0", false)} kw";
    public float PowerCh => Power / 1000 * 1.358f;
    public string PowerChDisplay => $"{DisplayMethods.PrintFloat(PowerCh, "0", false)} ch";
    public float TorqueNm { get; set; }
    public string TorqueNmDisplay => $"{DisplayMethods.PrintFloat(TorqueNm, "0", false)} nm";
    public float TorqueFtLbs => TorqueNm / 1.356f;
    public string TorqueFtLbsDisplay => $"{DisplayMethods.PrintFloat(TorqueFtLbs, "0", false)} ft lb";

    public int Gear { get; set; }

    // car
    public string CarClass { get; set; }
    public int CarIndex { get; set; }
    public string CarIndexDisplay => $"{CarClass} {CarIndex}";
    public string CarDrivetrain { get; set; }
    public int CylindersCount { get; set; }

    // car model
    public string Model { get; set; }
    public string Maker { get; set; }
    public int Year { get; set; }
    public string Group { get; set; }
    public int CarOrdinal { get; set; }
    public int Weight { get; set; }
}
