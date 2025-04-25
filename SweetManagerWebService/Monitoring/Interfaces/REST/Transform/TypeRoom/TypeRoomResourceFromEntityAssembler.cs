using SweetManagerWebService.Monitoring.Interfaces.REST.Resources.TypeRoom;

namespace SweetManagerWebService.Monitoring.Interfaces.REST.Transform.TypeRoom
{
    public class TypeRoomResourceFromEntityAssembler
    {
        public static TypeRoomResource ToResourceFromEntity
            (Domain.Model.Entities.TypeRoom entity) =>
            new(entity.Id, entity.Description, entity.Price);
    }
}