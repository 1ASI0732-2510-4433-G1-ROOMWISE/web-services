using SweetManagerWebService.Monitoring.Domain.Model.ValueObjects.Room;

namespace SweetManagerWebService.Monitoring.Domain.Model.Commands.Room
{
    public record UpdateRoomStateCommand
        (int Id, ERoomState RoomState);
}