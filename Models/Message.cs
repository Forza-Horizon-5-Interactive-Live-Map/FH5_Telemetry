using System;
using System.Collections.Generic;
using System.Text;

namespace ForzaDynamicMapApi.Models;

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
    public DateTime ReceivedTime { get; }

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

    public UInt16 LapNumber { get; set; }
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
        this.Ip = ip;
        this.PlayerName = playerName;

        switch (rawTelemetryData.Length)
        {
            case (311):
                Game = EGame.FM7;
                break;
            default:
            case (324):
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

        this.ReceivedTime = DateTime.UtcNow;

        this.IsRaceOn = BitConverter.ToInt32(rawTelemetryData, 0);
        this.TimestampMS = BitConverter.ToUInt32(rawTelemetryData, 4);
        this.EngineMaxRpm = BitConverter.ToSingle(rawTelemetryData, 8);
        this.EngineIdleRpm = BitConverter.ToSingle(rawTelemetryData, 12);
        this.CurrentEngineRpm = BitConverter.ToSingle(rawTelemetryData, 16);
        this.AccelerationX = BitConverter.ToSingle(rawTelemetryData, 20);
        this.AccelerationY = BitConverter.ToSingle(rawTelemetryData, 24);
        this.AccelerationZ = BitConverter.ToSingle(rawTelemetryData, 28);
        this.VelocityX = BitConverter.ToSingle(rawTelemetryData, 32);
        this.VelocityY = BitConverter.ToSingle(rawTelemetryData, 36);
        this.VelocityZ = BitConverter.ToSingle(rawTelemetryData, 40);
        this.AngularVelocityX = BitConverter.ToSingle(rawTelemetryData, 44);
        this.AngularVelocityY = BitConverter.ToSingle(rawTelemetryData, 48);
        this.AngularVelocityZ = BitConverter.ToSingle(rawTelemetryData, 52);
        this.Yaw = BitConverter.ToSingle(rawTelemetryData, 56);
        this.Pitch = BitConverter.ToSingle(rawTelemetryData, 60);
        this.Roll = BitConverter.ToSingle(rawTelemetryData, 64);
        this.NormalizedSuspensionTravelFrontLeft = BitConverter.ToSingle(rawTelemetryData, 68);
        this.NormalizedSuspensionTravelFrontRight = BitConverter.ToSingle(rawTelemetryData, 72);
        this.NormalizedSuspensionTravelRearLeft = BitConverter.ToSingle(rawTelemetryData, 76);
        this.NormalizedSuspensionTravelRearRight = BitConverter.ToSingle(rawTelemetryData, 80);
        this.TireSlipRatioFrontLeft = BitConverter.ToSingle(rawTelemetryData, 84);
        this.TireSlipRatioFrontRight = BitConverter.ToSingle(rawTelemetryData, 88);
        this.TireSlipRatioRearLeft = BitConverter.ToSingle(rawTelemetryData, 92);
        this.TireSlipRatioRearRight = BitConverter.ToSingle(rawTelemetryData, 96);
        this.WheelRotationSpeedFrontLeft = BitConverter.ToSingle(rawTelemetryData, 100);
        this.WheelRotationSpeedFrontRight = BitConverter.ToSingle(rawTelemetryData, 104);
        this.WheelRotationSpeedRearLeft = BitConverter.ToSingle(rawTelemetryData, 108);
        this.WheelRotationSpeedRearRight = BitConverter.ToSingle(rawTelemetryData, 112);
        this.WheelOnRumbleStripFrontLeft = BitConverter.ToInt32(rawTelemetryData, 116);
        this.WheelOnRumbleStripFrontRight = BitConverter.ToInt32(rawTelemetryData, 120);
        this.WheelOnRumbleStripRearLeft = BitConverter.ToInt32(rawTelemetryData, 124);
        this.WheelOnRumbleStripRearRight = BitConverter.ToInt32(rawTelemetryData, 128);
        this.WheelInPuddleDepthFrontLeft = BitConverter.ToSingle(rawTelemetryData, 132);
        this.WheelInPuddleDepthFrontRight = BitConverter.ToSingle(rawTelemetryData, 136);
        this.WheelInPuddleDepthRearLeft = BitConverter.ToSingle(rawTelemetryData, 140);
        this.WheelInPuddleDepthRearRight = BitConverter.ToSingle(rawTelemetryData, 144);
        this.SurfaceRumbleFrontLeft = BitConverter.ToSingle(rawTelemetryData, 148);
        this.SurfaceRumbleFrontRight = BitConverter.ToSingle(rawTelemetryData, 152);
        this.SurfaceRumbleRearLeft = BitConverter.ToSingle(rawTelemetryData, 156);
        this.SurfaceRumbleRearRight = BitConverter.ToSingle(rawTelemetryData, 160);
        this.TireSlipAngleFrontLeft = BitConverter.ToSingle(rawTelemetryData, 164);
        this.TireSlipAngleFrontRight = BitConverter.ToSingle(rawTelemetryData, 168);
        this.TireSlipAngleRearLeft = BitConverter.ToSingle(rawTelemetryData, 172);
        this.TireSlipAngleRearRight = BitConverter.ToSingle(rawTelemetryData, 176);
        this.TireCombinedSlipFrontLeft = BitConverter.ToSingle(rawTelemetryData, 180);
        this.TireCombinedSlipFrontRight = BitConverter.ToSingle(rawTelemetryData, 184);
        this.TireCombinedSlipRearLeft = BitConverter.ToSingle(rawTelemetryData, 188);
        this.TireCombinedSlipRearRight = BitConverter.ToSingle(rawTelemetryData, 192);
        this.SuspensionTravelMetersFrontLeft = BitConverter.ToSingle(rawTelemetryData, 196);
        this.SuspensionTravelMetersFrontRight = BitConverter.ToSingle(rawTelemetryData, 200);
        this.SuspensionTravelMetersRearLeft = BitConverter.ToSingle(rawTelemetryData, 204);
        this.SuspensionTravelMetersRearRight = BitConverter.ToSingle(rawTelemetryData, 208);
        this.CarOrdinal = BitConverter.ToInt32(rawTelemetryData, 212);
        this.CarClass = (ECarClass)BitConverter.ToInt32(rawTelemetryData, 216);
        this.CarPerformanceIndex = BitConverter.ToInt32(rawTelemetryData, 220);
        this.DrivetrainType = (EDrivetrainType)BitConverter.ToInt32(rawTelemetryData, 224);
        this.NumCylinders = BitConverter.ToInt32(rawTelemetryData, 228);

        //V2
        this.PositionX = BitConverter.ToSingle(rawTelemetryData, 232); //Position (meters)
        this.PositionY = BitConverter.ToSingle(rawTelemetryData, 236);
        this.PositionZ = BitConverter.ToSingle(rawTelemetryData, 240);
        this.Speed = BitConverter.ToSingle(rawTelemetryData, 244); //m/s
        this.Power = BitConverter.ToSingle(rawTelemetryData, 248); //watts
        this.Torque = BitConverter.ToSingle(rawTelemetryData, 252); // newton meter

        this.TireTempFrontLeft = BitConverter.ToSingle(rawTelemetryData, 256);
        this.TireTempFrontRight = BitConverter.ToSingle(rawTelemetryData, 260);
        this.TireTempRearLeft = BitConverter.ToSingle(rawTelemetryData, 264);
        this.TireTempRearRight = BitConverter.ToSingle(rawTelemetryData, 268);

        this.Boost = BitConverter.ToSingle(rawTelemetryData, 272);
        this.Fuel = BitConverter.ToSingle(rawTelemetryData, 276);
        this.DistanceTraveled = BitConverter.ToSingle(rawTelemetryData, 280);
        this.BestLap = BitConverter.ToSingle(rawTelemetryData, 284);
        this.LastLap = BitConverter.ToSingle(rawTelemetryData, 288);
        this.CurrentLap = BitConverter.ToSingle(rawTelemetryData, 292);
        this.CurrentRaceTime = BitConverter.ToSingle(rawTelemetryData, 296);

        this.LapNumber = BitConverter.ToUInt16(rawTelemetryData, 300);

        this.RacePosition = rawTelemetryData[302];

        this.Accel = rawTelemetryData[303];
        this.Brake = rawTelemetryData[304];
        this.Clutch = rawTelemetryData[305];
        this.HandBrake = rawTelemetryData[306];
        this.Gear = rawTelemetryData[307];
        this.Steer = (sbyte)rawTelemetryData[308];

        this.NormalizedDrivingLine = (sbyte)rawTelemetryData[309];
        this.NormalizedAIBrakeDifference = (sbyte)rawTelemetryData[310];
    }

    #region Methods

    public float GetGAccel()
    {
        double aX = this.AccelerationX;
        double aY = this.AccelerationY;
        double aZ = this.AccelerationZ;

        var res = Math.Sqrt(aZ * aZ + aY * aY + aX * aX) / 9.81;
        return (float)res;
    }

    #endregion
}
