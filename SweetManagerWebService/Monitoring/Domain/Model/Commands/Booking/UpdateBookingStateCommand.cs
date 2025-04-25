using SweetManagerWebService.Monitoring.Domain.Model.ValueObjects.Booking;

namespace SweetManagerWebService.Monitoring.Domain.Model.Commands.Booking
{
    public record UpdateBookingStateCommand
        (int Id, EBookingState BookingState);
}