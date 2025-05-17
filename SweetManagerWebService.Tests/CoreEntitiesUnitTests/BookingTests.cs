using SweetManagerWebService.Monitoring.Domain.Model.Aggregates;
using SweetManagerWebService.Monitoring.Domain.Model.Commands.Booking;
using SweetManagerWebService.Monitoring.Domain.Model.ValueObjects.Booking;

namespace SweetManagerWebService.Tests.CoreEntitiesUnitTests;

[TestFixture]
public class BookingTests
{
    [Test]
    public void Booking_Constructor_WithParameters_ShouldInitializeProperties()
    {
        // Arrange
        var paymentCustomerId = 1;
        var roomId = 2;
        var description = "Reserva para vacaciones";
        var startDate = new DateTime(2025, 6, 15);
        var finalDate = new DateTime(2025, 6, 20);
        var priceRoom = 120.00m;
        var nightCount = 5;
        var bookingState = EBookingState.RESERVADO;

        // Act
        var booking = new Booking(
            paymentCustomerId,
            roomId,
            description,
            startDate,
            finalDate,
            priceRoom,
            nightCount,
            bookingState
        );

        // Assert
        Assert.That(booking.PaymentsCustomersId, Is.EqualTo(paymentCustomerId));
        Assert.That(booking.RoomsId, Is.EqualTo(roomId));
        Assert.That(booking.Description, Is.EqualTo(description));
        Assert.That(booking.StartDate, Is.EqualTo(startDate));
        Assert.That(booking.FinalDate, Is.EqualTo(finalDate));
        Assert.That(booking.PriceRoom, Is.EqualTo(priceRoom));
        Assert.That(booking.NightCount, Is.EqualTo(nightCount));
        Assert.That(booking.Amount, Is.EqualTo(priceRoom * nightCount));
        Assert.That(booking.State, Is.EqualTo(bookingState.ToString()));
    }

    [Test]
    public void Booking_Constructor_WithCommand_ShouldInitializeProperties()
    {
        // Arrange
        var command = new CreateBookingCommand(
            PaymentCustomerId: 3,
            RoomId: 4,
            Description: "Reserva para conferencia",
            StartDate: new DateTime(2025, 7, 10),
            FinalDate: new DateTime(2025, 7, 12),
            PriceRoom: 150.00m,
            NightCount: 2,
            BookingState: EBookingState.PAGADO
        );

        // Act
        var booking = new Booking(command);

        // Assert
        Assert.That(booking.PaymentsCustomersId, Is.EqualTo(command.PaymentCustomerId));
        Assert.That(booking.RoomsId, Is.EqualTo(command.RoomId));
        Assert.That(booking.Description, Is.EqualTo(command.Description));
        Assert.That(booking.StartDate, Is.EqualTo(command.StartDate));
        Assert.That(booking.FinalDate, Is.EqualTo(command.FinalDate));
        Assert.That(booking.PriceRoom, Is.EqualTo(command.PriceRoom));
        Assert.That(booking.NightCount, Is.EqualTo(command.NightCount));
        Assert.That(booking.Amount, Is.EqualTo(command.PriceRoom * command.NightCount));
        Assert.That(booking.State, Is.EqualTo(command.BookingState.ToString()));
    }

    

    
}