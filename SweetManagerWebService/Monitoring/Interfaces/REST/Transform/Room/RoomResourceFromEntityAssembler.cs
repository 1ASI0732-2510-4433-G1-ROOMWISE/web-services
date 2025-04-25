using SweetManagerWebService.Monitoring.Interfaces.REST.Resources.Room;

namespace SweetManagerWebService.Monitoring.Interfaces.REST.Transform.Room
{
    public class RoomResourceFromEntityAssembler
    {
        public static RoomResource ToResourceFromEntity
            (Domain.Model.Aggregates.Room entity) =>
            new(entity.Id, entity.TypesRoomsId,
                entity.HotelsId, entity.State);
    }
}