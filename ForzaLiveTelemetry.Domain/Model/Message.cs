namespace ForzaLiveTelemetry.Domain.Model;

public enum EGame
{
    FM7 = 0,
    FH4 = 1,
    FH5 = 2
}
public enum ECarClass
{
    D = 0,
    C = 1,
    B = 2,
    A = 3,
    S1 = 4,
    S2 = 5,
    X = 6
}
public enum EDrivetrainType
{
    FWD = 0,
    RWD = 1,
    AWD = 2
}

public class Message
{
    public string Ip { get; set; }
    public string PlayerName { get; set; }
    public DateTime ReceivedTime { get; set; }

    #region Telemetry Property

    public EGame Game { get; set; }
    public int IsRaceOn { get; set; } // = 1 when race is on. = 0 when in menus/race stopped …
    public uint TimestampMS { get; set; } //Can overflow to 0 eventually
    public float EngineMaxRpm { get; set; }
    public float EngineIdleRpm { get; set; }
    public float CurrentEngineRpm { get; set; }
    public float AccelerationX { get; set; } //m/s^2
    public float AccelerationY { get; set; }
    public float AccelerationZ { get; set; }
    public float VelocityX { get; set; } //In the car's local space; X = right, Y = up, Z = forward
    public float VelocityY { get; set; }
    public float VelocityZ { get; set; }
    public float AngularVelocityX { get; set; } //In the car's local space; X = pitch, Y = yaw, Z = roll
    public float AngularVelocityY { get; set; }
    public float AngularVelocityZ { get; set; }
    public float Yaw { get; set; }
    public float Pitch { get; set; }
    public float Roll { get; set; }
    public float NormalizedSuspensionTravelFrontLeft { get; set; } // Suspension travel normalized: 0.0f = max stretch; 1.0 = max compression
    public float NormalizedSuspensionTravelFrontRight { get; set; }
    public float NormalizedSuspensionTravelRearLeft { get; set; }
    public float NormalizedSuspensionTravelRearRight { get; set; }
    public float TireSlipRatioFrontLeft { get; set; } // Tire normalized slip ratio, = 0 means 100% grip and |ratio| > 1.0 means loss of grip.
    public float TireSlipRatioFrontRight { get; set; }
    public float TireSlipRatioRearLeft { get; set; }
    public float TireSlipRatioRearRight { get; set; }
    public float WheelRotationSpeedFrontLeft { get; set; } // Wheel rotation speed radians/sec. 
    public float WheelRotationSpeedFrontRight { get; set; }
    public float WheelRotationSpeedRearLeft { get; set; }
    public float WheelRotationSpeedRearRight { get; set; }
    public int WheelOnRumbleStripFrontLeft { get; set; } // = 1 when wheel is on rumble strip, = 0 when off.
    public int WheelOnRumbleStripFrontRight { get; set; }
    public int WheelOnRumbleStripRearLeft { get; set; }
    public int WheelOnRumbleStripRearRight { get; set; }
    public float WheelInPuddleDepthFrontLeft { get; set; } // = from 0 to 1, where 1 is the deepest puddle
    public float WheelInPuddleDepthFrontRight { get; set; }
    public float WheelInPuddleDepthRearLeft { get; set; }
    public float WheelInPuddleDepthRearRight { get; set; }
    public float SurfaceRumbleFrontLeft { get; set; } // Non-dimensional surface rumble values passed to controller force feedback
    public float SurfaceRumbleFrontRight { get; set; }
    public float SurfaceRumbleRearLeft { get; set; }
    public float SurfaceRumbleRearRight { get; set; }
    public float TireSlipAngleFrontLeft { get; set; } // Tire normalized slip angle, = 0 means 100% grip and |angle| > 1.0 means loss of grip.
    public float TireSlipAngleFrontRight { get; set; }
    public float TireSlipAngleRearLeft { get; set; }
    public float TireSlipAngleRearRight { get; set; }
    public float TireCombinedSlipFrontLeft { get; set; } // Tire normalized combined slip, = 0 means 100% grip and |slip| > 1.0 means loss of grip.
    public float TireCombinedSlipFrontRight { get; set; }
    public float TireCombinedSlipRearLeft { get; set; }
    public float TireCombinedSlipRearRight { get; set; }
    public float SuspensionTravelMetersFrontLeft { get; set; } // Actual suspension travel in meters
    public float SuspensionTravelMetersFrontRight { get; set; }
    public float SuspensionTravelMetersRearLeft { get; set; }
    public float SuspensionTravelMetersRearRight { get; set; }
    public int CarOrdinal { get; set; } //Unique ID of the car make/model
    public ECarClass CarClass { get; set; } //Between 0 (D -- worst cars) and 7 (X class -- best cars) inclusive 
    public int CarPerformanceIndex { get; set; } //Between 100 (slowest car) and 999 (fastest car) inclusive
    public EDrivetrainType DrivetrainType { get; set; } //Corresponds to EDrivetrainType; 0 = FWD, 1 = RWD, 2 = AWD
    public int NumCylinders { get; set; } //Number of cylinders in the engine

    /*
     * V2
     */
    public float PositionX { get; set; } //Position (meters)
    public float PositionY { get; set; }
    public float PositionZ { get; set; }
    public float Speed { get; set; } //m/s
    public float Power { get; set; } //watts
    public float Torque { get; set; } // newton meter

    public float TireTempFrontLeft { get; set; }
    public float TireTempFrontRight { get; set; }
    public float TireTempRearLeft { get; set; }
    public float TireTempRearRight { get; set; }

    public float Boost { get; set; }
    public float Fuel { get; set; }
    public float DistanceTraveled { get; set; }
    public float BestLap { get; set; }
    public float LastLap { get; set; }
    public float CurrentLap { get; set; }
    public float CurrentRaceTime { get; set; }

    public ushort LapNumber { get; set; }
    public byte RacePosition { get; set; }

    public byte Accel { get; set; }
    public byte Brake { get; set; }
    public byte Clutch { get; set; }
    public byte HandBrake { get; set; }
    public byte Gear { get; set; }
    public sbyte Steer { get; set; }

    public sbyte NormalizedDrivingLine { get; set; }
    public sbyte NormalizedAIBrakeDifference { get; set; }

    #endregion

    public Message() { }
    public Message(string ip, string playerName, byte[] rawTelemetryData)
    {
        Ip = ip;
        PlayerName = playerName;

        switch (rawTelemetryData.Length)
        {
            case 311:
                Game = EGame.FM7;
                break;
            default:
            case 324:
                Game = EGame.FH4;
                break;
        }

        if (Game == EGame.FH4)
        {
            byte[] newData = new byte[311];
            Array.Copy(rawTelemetryData, 0, newData, 0, 232);
            Array.Copy(rawTelemetryData, 244, newData, 232, 79);
            rawTelemetryData = newData;
        }

        ReceivedTime = DateTime.UtcNow;

        IsRaceOn = BitConverter.ToInt32(rawTelemetryData, 0);
        TimestampMS = BitConverter.ToUInt32(rawTelemetryData, 4);
        EngineMaxRpm = BitConverter.ToSingle(rawTelemetryData, 8);
        EngineIdleRpm = BitConverter.ToSingle(rawTelemetryData, 12);
        CurrentEngineRpm = BitConverter.ToSingle(rawTelemetryData, 16);
        AccelerationX = BitConverter.ToSingle(rawTelemetryData, 20);
        AccelerationY = BitConverter.ToSingle(rawTelemetryData, 24);
        AccelerationZ = BitConverter.ToSingle(rawTelemetryData, 28);
        VelocityX = BitConverter.ToSingle(rawTelemetryData, 32);
        VelocityY = BitConverter.ToSingle(rawTelemetryData, 36);
        VelocityZ = BitConverter.ToSingle(rawTelemetryData, 40);
        AngularVelocityX = BitConverter.ToSingle(rawTelemetryData, 44);
        AngularVelocityY = BitConverter.ToSingle(rawTelemetryData, 48);
        AngularVelocityZ = BitConverter.ToSingle(rawTelemetryData, 52);
        Yaw = BitConverter.ToSingle(rawTelemetryData, 56);
        Pitch = BitConverter.ToSingle(rawTelemetryData, 60);
        Roll = BitConverter.ToSingle(rawTelemetryData, 64);
        NormalizedSuspensionTravelFrontLeft = BitConverter.ToSingle(rawTelemetryData, 68);
        NormalizedSuspensionTravelFrontRight = BitConverter.ToSingle(rawTelemetryData, 72);
        NormalizedSuspensionTravelRearLeft = BitConverter.ToSingle(rawTelemetryData, 76);
        NormalizedSuspensionTravelRearRight = BitConverter.ToSingle(rawTelemetryData, 80);
        TireSlipRatioFrontLeft = BitConverter.ToSingle(rawTelemetryData, 84);
        TireSlipRatioFrontRight = BitConverter.ToSingle(rawTelemetryData, 88);
        TireSlipRatioRearLeft = BitConverter.ToSingle(rawTelemetryData, 92);
        TireSlipRatioRearRight = BitConverter.ToSingle(rawTelemetryData, 96);
        WheelRotationSpeedFrontLeft = BitConverter.ToSingle(rawTelemetryData, 100);
        WheelRotationSpeedFrontRight = BitConverter.ToSingle(rawTelemetryData, 104);
        WheelRotationSpeedRearLeft = BitConverter.ToSingle(rawTelemetryData, 108);
        WheelRotationSpeedRearRight = BitConverter.ToSingle(rawTelemetryData, 112);
        WheelOnRumbleStripFrontLeft = BitConverter.ToInt32(rawTelemetryData, 116);
        WheelOnRumbleStripFrontRight = BitConverter.ToInt32(rawTelemetryData, 120);
        WheelOnRumbleStripRearLeft = BitConverter.ToInt32(rawTelemetryData, 124);
        WheelOnRumbleStripRearRight = BitConverter.ToInt32(rawTelemetryData, 128);
        WheelInPuddleDepthFrontLeft = BitConverter.ToSingle(rawTelemetryData, 132);
        WheelInPuddleDepthFrontRight = BitConverter.ToSingle(rawTelemetryData, 136);
        WheelInPuddleDepthRearLeft = BitConverter.ToSingle(rawTelemetryData, 140);
        WheelInPuddleDepthRearRight = BitConverter.ToSingle(rawTelemetryData, 144);
        SurfaceRumbleFrontLeft = BitConverter.ToSingle(rawTelemetryData, 148);
        SurfaceRumbleFrontRight = BitConverter.ToSingle(rawTelemetryData, 152);
        SurfaceRumbleRearLeft = BitConverter.ToSingle(rawTelemetryData, 156);
        SurfaceRumbleRearRight = BitConverter.ToSingle(rawTelemetryData, 160);
        TireSlipAngleFrontLeft = BitConverter.ToSingle(rawTelemetryData, 164);
        TireSlipAngleFrontRight = BitConverter.ToSingle(rawTelemetryData, 168);
        TireSlipAngleRearLeft = BitConverter.ToSingle(rawTelemetryData, 172);
        TireSlipAngleRearRight = BitConverter.ToSingle(rawTelemetryData, 176);
        TireCombinedSlipFrontLeft = BitConverter.ToSingle(rawTelemetryData, 180);
        TireCombinedSlipFrontRight = BitConverter.ToSingle(rawTelemetryData, 184);
        TireCombinedSlipRearLeft = BitConverter.ToSingle(rawTelemetryData, 188);
        TireCombinedSlipRearRight = BitConverter.ToSingle(rawTelemetryData, 192);
        SuspensionTravelMetersFrontLeft = BitConverter.ToSingle(rawTelemetryData, 196);
        SuspensionTravelMetersFrontRight = BitConverter.ToSingle(rawTelemetryData, 200);
        SuspensionTravelMetersRearLeft = BitConverter.ToSingle(rawTelemetryData, 204);
        SuspensionTravelMetersRearRight = BitConverter.ToSingle(rawTelemetryData, 208);
        CarOrdinal = BitConverter.ToInt32(rawTelemetryData, 212);
        CarClass = (ECarClass)BitConverter.ToInt32(rawTelemetryData, 216);
        CarPerformanceIndex = BitConverter.ToInt32(rawTelemetryData, 220);
        DrivetrainType = (EDrivetrainType)BitConverter.ToInt32(rawTelemetryData, 224);
        NumCylinders = BitConverter.ToInt32(rawTelemetryData, 228);

        //V2
        PositionX = BitConverter.ToSingle(rawTelemetryData, 232); //Position (meters)
        PositionY = BitConverter.ToSingle(rawTelemetryData, 236);
        PositionZ = BitConverter.ToSingle(rawTelemetryData, 240);
        Speed = BitConverter.ToSingle(rawTelemetryData, 244); //m/s
        Power = BitConverter.ToSingle(rawTelemetryData, 248); //watts
        Torque = BitConverter.ToSingle(rawTelemetryData, 252); // newton meter

        TireTempFrontLeft = BitConverter.ToSingle(rawTelemetryData, 256);
        TireTempFrontRight = BitConverter.ToSingle(rawTelemetryData, 260);
        TireTempRearLeft = BitConverter.ToSingle(rawTelemetryData, 264);
        TireTempRearRight = BitConverter.ToSingle(rawTelemetryData, 268);

        Boost = BitConverter.ToSingle(rawTelemetryData, 272);
        Fuel = BitConverter.ToSingle(rawTelemetryData, 276);
        DistanceTraveled = BitConverter.ToSingle(rawTelemetryData, 280);
        BestLap = BitConverter.ToSingle(rawTelemetryData, 284);
        LastLap = BitConverter.ToSingle(rawTelemetryData, 288);
        CurrentLap = BitConverter.ToSingle(rawTelemetryData, 292);
        CurrentRaceTime = BitConverter.ToSingle(rawTelemetryData, 296);

        LapNumber = BitConverter.ToUInt16(rawTelemetryData, 300);

        RacePosition = rawTelemetryData[302];

        Accel = rawTelemetryData[303];
        Brake = rawTelemetryData[304];
        Clutch = rawTelemetryData[305];
        HandBrake = rawTelemetryData[306];
        Gear = rawTelemetryData[307];
        Steer = (sbyte)rawTelemetryData[308];

        NormalizedDrivingLine = (sbyte)rawTelemetryData[309];
        NormalizedAIBrakeDifference = (sbyte)rawTelemetryData[310];
    }

    #region Methods

    public float GetGAccel()
    {
        double aX = AccelerationX;
        double aY = AccelerationY;
        double aZ = AccelerationZ;

        var res = Math.Sqrt(aZ * aZ + aY * aY + aX * aX) / 9.81;
        return (float)res;
    }

    #endregion
}
