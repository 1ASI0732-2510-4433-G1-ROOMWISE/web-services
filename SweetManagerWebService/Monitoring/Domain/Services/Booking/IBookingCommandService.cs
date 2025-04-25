using SweetManagerWebService.Monitoring.Domain.Model.Commands.Booking;

namespace SweetManagerWebService.Monitoring.Domain.Services.Booking
{
    public interface IBookingCommandService
    {
        Task<bool> Handle
            (CreateBookingCommand command);

        Task<bool> Handle
            (UpdateBookingStateCommand command);
    }
}