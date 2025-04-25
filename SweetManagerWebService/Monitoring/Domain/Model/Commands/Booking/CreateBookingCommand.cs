using SweetManagerWebService.Monitoring.Domain.Model.ValueObjects.Booking;

namespace SweetManagerWebService.Monitoring.Domain.Model.Commands.Booking
{
    public record CreateBookingCommand
        (int PaymentCustomerId, int RoomId,
        string Description, DateTime StartDate,
        DateTime FinalDate, decimal PriceRoom,
        int NightCount, EBookingState BookingState);
}