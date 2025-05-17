using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SweetManagerWebService.Monitoring.Domain.Model.Aggregates;
using SweetManagerWebService.Monitoring.Domain.Model.Commands.Booking;
using SweetManagerWebService.Monitoring.Domain.Model.Queries.Booking;
using SweetManagerWebService.Monitoring.Domain.Model.ValueObjects.Booking;
using SweetManagerWebService.Monitoring.Domain.Services.Booking;
using SweetManagerWebService.Monitoring.Interfaces.REST;
using SweetManagerWebService.Monitoring.Interfaces.REST.Resources.Booking;


namespace SweetManagerWebService.Tests.CoreIntegrationTests
{
    [TestFixture]
    public class BookingsControllerTests
    {
        // ✅ Test 1: Crear booking correctamente
        [Test]
        public async Task CreateBooking_ReturnsOk()
        {
            var mockCommand = new Mock<IBookingCommandService>();
            var mockQuery = new Mock<IBookingQueryService>();

            var resource = new CreateBookingResource(1, 1, "Reserva de prueba", "2025-06-01", "2025-06-05", 100m, 4);

            mockCommand.Setup(s => s.Handle(It.IsAny<CreateBookingCommand>())).ReturnsAsync(true);

            var controller = new BookingsController(mockCommand.Object, mockQuery.Object);

            var result = await controller.CreateBooking(resource);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(((OkObjectResult)result).Value, Is.EqualTo(true));
        }

      

        // ✅ Test 3: Obtener booking por ID correctamente
        [Test]
        public async Task GetBookingById_ReturnsBooking()
        {
            var mockCommand = new Mock<IBookingCommandService>();
            var mockQuery = new Mock<IBookingQueryService>();

            var booking = new Booking(1, 1, "Reserva de prueba", new DateTime(2025, 6, 1), new DateTime(2025, 6, 5), 100m, 4, EBookingState.RESERVADO);

            mockQuery.Setup(s => s.Handle(It.Is<GetBookingByIdQuery>(q => q.Id == 10)))
                     .ReturnsAsync(booking);

            var controller = new BookingsController(mockCommand.Object, mockQuery.Object);

            var result = await controller.BookingById(10);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var returned = (BookingResource)((OkObjectResult)result).Value;

            Assert.That(returned.PaymentCustomerId, Is.EqualTo(1));
            Assert.That(returned.RoomId, Is.EqualTo(1));
            Assert.That(returned.BookingState, Is.EqualTo("RESERVADO"));
        }

        

        // ✅ Test 5: No se encuentra booking por ID
        [Test]
        public async Task GetBookingById_NotFound_ReturnsBadRequest()
        {
            var mockCommand = new Mock<IBookingCommandService>();
            var mockQuery = new Mock<IBookingQueryService>();

            mockQuery.Setup(s => s.Handle(It.IsAny<GetBookingByIdQuery>()))
                     .ReturnsAsync((Booking)null);

            var controller = new BookingsController(mockCommand.Object, mockQuery.Object);

            var result = await controller.BookingById(999);

            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }
    }
}
