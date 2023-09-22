using ForzaDynamicMapApi.DTO.Messages;
using ForzaDynamicMapApi.Models;

namespace ForzaDynamicMapApi.Mappers;

public static class MessagesMapper
{
    public static MessageDTO ToDTO(this Message message, CarInfo info)
    {
        return new MessageDTO()
        {
            Ip = message.Ip,
            PlayerName = message.PlayerName,
            IsPaused = message.IsRaceOn == 0,
            IsDisconnecting = DateTime.UtcNow - message.ReceivedTime > TimeSpan.FromSeconds(5),
            PosX = message.PositionX,
            PosY = message.PositionY,
            PosZ = message.PositionZ,
            Speed = message.Speed,
            Model = info.Model,
            Maker = info.Maker,
            Year = info.Year,
            Group = info.Group,
            CarOrdinal = info.CarOrdinal,
            Weight = info.Weight,
        };
    }
}
