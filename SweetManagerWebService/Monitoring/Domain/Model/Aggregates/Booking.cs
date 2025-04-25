using SweetManagerWebService.Commerce.Domain.Model.Entities.Payments;
using SweetManagerWebService.Monitoring.Domain.Model.Commands.Booking;
using SweetManagerWebService.Monitoring.Domain.Model.ValueObjects.Booking;

namespace SweetManagerWebService.Monitoring.Domain.Model.Aggregates
{
    public partial class Booking
    {
        public int Id { get; }
        public int PaymentsCustomersId { get; set; }
        public int RoomsId { get; set; }
        public string Description { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime FinalDate { get; set; }
        public decimal PriceRoom { get; set; }
        public int NightCount { get; set; }
        public decimal Amount { get; set; }
        public string State { get; set; } = null!;

        public virtual PaymentCustomer PaymentCustomer { get; } = null!;
        public virtual Room Room { get; } = null!;

        public Booking()
        {
            this.PaymentsCustomersId = 0;
            this.RoomsId = 0;
            this.Description = string.Empty;
            this.PriceRoom = 0;
            this.NightCount = 0;
            this.Amount = 0;
            this.State = string.Empty;
        }
        public Booking
            (int paymentCustomerId, int roomId, string description,
            DateTime startDate, DateTime finalDate, decimal priceRoom,
            int nightCount, EBookingState bookingState)
        {
            this.PaymentsCustomersId = paymentCustomerId;
            this.RoomsId = roomId;
            this.Description = description;
            this.StartDate = startDate;
            this.FinalDate = finalDate;
            this.PriceRoom = priceRoom;
            this.NightCount = nightCount;
            this.Amount = priceRoom * nightCount;
            this.State = bookingState.ToString();
        }
        public Booking
            (CreateBookingCommand command)
        {
            this.PaymentsCustomersId = command.PaymentCustomerId;
            this.RoomsId = command.RoomId;
            this.Description = command.Description;
            this.StartDate = command.StartDate;
            this.FinalDate = command.FinalDate;
            this.PriceRoom = command.PriceRoom;
            this.NightCount = command.NightCount;
            this.Amount = command.PriceRoom * NightCount;
            this.State = command.BookingState.ToString();
        }
        public Booking
            (UpdateBookingStateCommand command)
        {
            this.Id = command.Id;
            this.State = command.BookingState.ToString();
        }
    }
}