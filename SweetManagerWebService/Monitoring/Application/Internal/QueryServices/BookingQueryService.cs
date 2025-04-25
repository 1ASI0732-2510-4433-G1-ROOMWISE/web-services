using SweetManagerWebService.Monitoring.Domain.Model.Aggregates;
using SweetManagerWebService.Monitoring.Domain.Model.Queries.Booking;
using SweetManagerWebService.Monitoring.Domain.Repositories;
using SweetManagerWebService.Monitoring.Domain.Services.Booking;

namespace SweetManagerWebService.Monitoring.Application.Internal.QueryServices
{
    public class BookingQueryService
        (IBookingRepository bookingRepository) :
        IBookingQueryService
    {
        public async Task<IEnumerable<Booking>> Handle
            (GetAllBookingsQuery query) =>
            await bookingRepository.FindAllByHotelIdAsync(query.HotelId);

        public async Task<Booking?> Handle
            (GetBookingByIdQuery query) =>
            await bookingRepository
            .FindByIdAsync(query.Id);
    }
}