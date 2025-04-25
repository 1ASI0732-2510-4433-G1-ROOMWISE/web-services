namespace SweetManagerWebService.Monitoring.Interfaces.REST.Resources.Booking
{
    public record CreateBookingResource
        (int PaymentCustomerId, int RoomId,
        string Description, string StartDate,
        string FinalDate, decimal PriceRoom,
        int NightCount);
}