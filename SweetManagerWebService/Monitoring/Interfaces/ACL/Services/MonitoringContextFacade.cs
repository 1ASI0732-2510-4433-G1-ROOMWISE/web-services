using SweetManagerWebService.Monitoring.Domain.Model.Queries.Booking;
using SweetManagerWebService.Monitoring.Domain.Model.Queries.Room;
using SweetManagerWebService.Monitoring.Domain.Services.Booking;
using SweetManagerWebService.Monitoring.Domain.Services.Room;

namespace SweetManagerWebService.Monitoring.Interfaces.ACL.Services
{
    public class MonitoringContextFacade
        (IBookingQueryService bookingQueryService,
        IRoomQueryService roomQueryService) :
        IMonitoringContextFacade
    {
        public async Task<bool> ExistsBookingById(int id) =>
            await bookingQueryService.Handle
            (new GetBookingByIdQuery(id)) != null;

        public async Task<bool> ExistsRoomById(int id) =>
            await roomQueryService.Handle
            (new GetRoomByIdQuery(id)) != null;

        public async Task<int> GetRoomsCount(int hotelId)
        {
            var rooms = await roomQueryService.Handle(new GetAllRoomsQuery(hotelId));

            return rooms.Count();
        }
    }
}