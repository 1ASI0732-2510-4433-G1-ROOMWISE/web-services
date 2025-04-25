using SweetManagerWebService.Monitoring.Domain.Model.ValueObjects.Room;

namespace SweetManagerWebService.Monitoring.Domain.Model.Commands.Room
{
    public record CreateRoomCommand
        (int TypeRoomId, int HotelId,
        ERoomState RoomState);
}