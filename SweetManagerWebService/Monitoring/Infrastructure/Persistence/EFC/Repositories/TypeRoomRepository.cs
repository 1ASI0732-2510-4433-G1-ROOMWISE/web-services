using SweetManagerWebService.Monitoring.Domain.Model.Aggregates;
using SweetManagerWebService.Monitoring.Domain.Model.Entities;
using SweetManagerWebService.Monitoring.Domain.Repositories;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerWebService.Monitoring.Infrastructure.Persistence.EFC.Repositories
{
    public class TypeRoomRepository(SweetManagerContext context) : BaseRepository<TypeRoom>(context), ITypeRoomRepository
    {
        public async Task<IEnumerable<TypeRoom>> FindAllByHotelId(int hotelId)
        {
            Task<IEnumerable<TypeRoom>> queryAsync = new (() => (
                from tr in Context.Set<TypeRoom>().ToList()
                join ro in Context.Set<Room>().ToList() on tr.Id equals ro.TypesRoomsId
                where ro.HotelsId.Equals(hotelId)
                select tr
            ).ToList());

            queryAsync.Start();

            var result = await queryAsync;

            return result;
        }
    }
}