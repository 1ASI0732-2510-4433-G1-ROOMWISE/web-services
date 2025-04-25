using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.Monitoring.Domain.Model.Aggregates;
using SweetManagerWebService.Monitoring.Domain.Model.ValueObjects.Booking;

namespace SweetManagerWebService.Monitoring.Domain.Repositories
{
    public interface IBookingRepository :
        IBaseRepository<Booking>
    {
        Task<bool> UpdateBookingStateAsync
            (int id, EBookingState bookingState);

        Task<IEnumerable<Booking>> FindAllByHotelIdAsync(int hotelId);
        
    }
}