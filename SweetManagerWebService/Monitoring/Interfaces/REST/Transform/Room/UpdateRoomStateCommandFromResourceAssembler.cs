using SweetManagerWebService.Monitoring.Domain.Model.Commands.Room;
using SweetManagerWebService.Monitoring.Domain.Model.ValueObjects.Room;
using SweetManagerWebService.Monitoring.Interfaces.REST.Resources.Room;

namespace SweetManagerWebService.Monitoring.Interfaces.REST.Transform.Room
{
    public class UpdateRoomStateCommandFromResourceAssembler
    {
        public static UpdateRoomStateCommand ToCommandFromResource
            (UpdateRoomStateResource resource) =>
            new(resource.Id, Enum.Parse<ERoomState>
                (resource.RoomState));
    }
}