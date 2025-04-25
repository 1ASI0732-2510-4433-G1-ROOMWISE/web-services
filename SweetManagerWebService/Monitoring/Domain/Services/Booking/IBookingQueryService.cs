using SweetManagerWebService.Monitoring.Domain.Model.Queries.Booking;

namespace SweetManagerWebService.Monitoring.Domain.Services.Booking
{
    public interface IBookingQueryService
    {
        Task<IEnumerable<Model.Aggregates.Booking>> Handle
            (GetAllBookingsQuery query);

        Task<Model.Aggregates.Booking?> Handle
            (GetBookingByIdQuery query);
    }
}