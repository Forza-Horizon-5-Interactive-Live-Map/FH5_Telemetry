using ForzaLiveTelemetry.Domain.DTO.Messages;
using ForzaLiveTelemetry.Domain.Model;

namespace ForzaLiveTelemetry.Domain.Mapper;

public static class MessagesMapper
{
    public static MessageDTO ToDTO(this Message message, CarInfo info)
    {
        return new MessageDTO()
        {
            Id = message.PlayerName+info.Model+info.Maker,
            PlayerName = message.PlayerName,
            IsPaused = message.IsRaceOn == 0,
            IsDisconnecting = DateTime.UtcNow - message.ReceivedTime > TimeSpan.FromSeconds(5),
            PosX = message.PositionX,
            PosY = message.PositionY,
            PosZ = message.PositionZ,
            GAcceleration = message.GetGAccel(),
            Yaw = message.Yaw,
            Pitch = message.Pitch,
            Roll = message.Roll,
            Speed = message.Speed,
            Power = message.Power,
            TorqueNm = message.Torque,
            Gear = message.Gear,
            CarClass = message.CarClass.ToString(),
            CarIndex = message.CarPerformanceIndex,
            CarDrivetrain = message.DrivetrainType.ToString(),
            CylindersCount = message.NumCylinders,
            Model = info.Model,
            Maker = info.Maker,
            Year = info.Year,
            Group = info.Group,
            CarOrdinal = info.CarOrdinal,
            Weight = info.Weight,
        };
    }
}
