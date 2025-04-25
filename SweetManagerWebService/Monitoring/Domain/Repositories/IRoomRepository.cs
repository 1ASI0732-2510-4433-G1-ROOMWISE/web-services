using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.Monitoring.Domain.Model.Aggregates;
using SweetManagerWebService.Monitoring.Domain.Model.ValueObjects.Room;

namespace SweetManagerWebService.Monitoring.Domain.Repositories
{
    public interface IRoomRepository :
        IBaseRepository<Room>
    {
        Task<bool> UpdateRoomStateAsync(int id, ERoomState roomState);

        Task<IEnumerable<Room>> FindByTypeRoomIdAsync(int typeRoomId);

        Task<IEnumerable<Room>> FindAllByHotelId(int hotelId);
    }
}