namespace SweetManagerWebService.Monitoring.Interfaces.REST.Resources.Booking
{
    public record BookingResource
        (int Id, int PaymentCustomerId, int RoomId,
        string Description, DateTime StartDate,
        DateTime FinalDate, decimal PriceRoom,
        int NightCount, decimal Amount,
        string BookingState);
}