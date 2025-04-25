using SweetManagerWebService.Monitoring.Domain.Model.Commands.TypeRoom;
using SweetManagerWebService.Monitoring.Interfaces.REST.Resources.TypeRoom;

namespace SweetManagerWebService.Monitoring.Interfaces.REST.Transform.TypeRoom
{
    public class CreateTypeCommandFromResourceAssembler
    {
        public static CreateTypeRoomCommand ToCommandFromResource
            (CreateTypeRoomResource resource) =>
            new(resource.Description, resource.Price);
    }
}