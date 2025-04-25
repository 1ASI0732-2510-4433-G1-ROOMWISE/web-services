using SweetManagerWebService.Monitoring.Domain.Model.Queries.TypeRoom;

namespace SweetManagerWebService.Monitoring.Domain.Services.TypeRoom
{
    public interface ITypeRoomQueryService
    {
        Task<IEnumerable<Model.Entities.TypeRoom>> Handle
            (GetAllTypesRoomsQuery query);

        Task<Model.Entities.TypeRoom?> Handle
            (GetTypeRoomByIdQuery query);
    }
}