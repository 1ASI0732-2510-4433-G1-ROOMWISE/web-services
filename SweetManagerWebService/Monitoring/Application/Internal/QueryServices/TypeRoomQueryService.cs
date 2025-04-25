using SweetManagerWebService.Monitoring.Domain.Model.Entities;
using SweetManagerWebService.Monitoring.Domain.Model.Queries.TypeRoom;
using SweetManagerWebService.Monitoring.Domain.Repositories;
using SweetManagerWebService.Monitoring.Domain.Services.TypeRoom;

namespace SweetManagerWebService.Monitoring.Application.Internal.QueryServices
{
    public class TypeRoomQueryService
        (ITypeRoomRepository typeRoomRepository) :
        ITypeRoomQueryService
    {
        public async Task<IEnumerable<TypeRoom>> Handle
            (GetAllTypesRoomsQuery query) =>
            await typeRoomRepository.FindAllByHotelId(query.HotelId);

        public async Task<TypeRoom?> Handle
            (GetTypeRoomByIdQuery query) =>
            await typeRoomRepository
            .FindByIdAsync(query.Id);
    }
}