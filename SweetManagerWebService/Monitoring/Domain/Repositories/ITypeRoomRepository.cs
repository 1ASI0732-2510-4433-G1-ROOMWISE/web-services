using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.Monitoring.Domain.Model.Entities;

namespace SweetManagerWebService.Monitoring.Domain.Repositories
{
    public interface ITypeRoomRepository :
        IBaseRepository<TypeRoom>
    {
        Task<IEnumerable<TypeRoom>> FindAllByHotelId(int hotelId);
        
    }
}