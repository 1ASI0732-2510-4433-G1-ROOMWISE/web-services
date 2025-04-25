using SweetManagerWebService.Monitoring.Domain.Model.Commands.Room;
using SweetManagerWebService.Monitoring.Domain.Model.ValueObjects.Room;
using SweetManagerWebService.Monitoring.Interfaces.REST.Resources.Room;

namespace SweetManagerWebService.Monitoring.Interfaces.REST.Transform.Room
{
    public class CreateRoomCommandFromResourceAssembler
    {
        public static CreateRoomCommand ToCommandFromResource
            (CreateRoomResource resource) =>
            new(resource.TypeRoomId, resource.HotelId,
                ERoomState.LIBRE);
    }
}