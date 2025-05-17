using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SweetManagerWebService.Monitoring.Domain.Model.Aggregates;
using SweetManagerWebService.Monitoring.Domain.Model.Commands.Room;
using SweetManagerWebService.Monitoring.Domain.Model.Queries.Room;
using SweetManagerWebService.Monitoring.Domain.Model.ValueObjects.Room;
using SweetManagerWebService.Monitoring.Domain.Services.Room;
using SweetManagerWebService.Monitoring.Interfaces.REST;
using SweetManagerWebService.Monitoring.Interfaces.REST.Resources.Room;


namespace SweetManagerWebService.Tests.CoreIntegrationTests
{
    [TestFixture]
    public class RoomsControllerTests
    {
        // ✅ Test 1: Crear habitación correctamente
        [Test]
        public async Task CreateRoom_ReturnsOk()
        {
            var mockCommand = new Mock<IRoomCommandService>();
            var mockQuery = new Mock<IRoomQueryService>();

            var resource = new CreateRoomResource(1, 1);

            mockCommand.Setup(s => s.Handle(It.IsAny<CreateRoomCommand>())).ReturnsAsync(true);

            var controller = new RoomsController(mockCommand.Object, mockQuery.Object);

            var result = await controller.CreateRoom(resource);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(((OkObjectResult)result).Value, Is.EqualTo(true));
        }

       

        // ✅ Test 3: Obtener habitación por ID
        [Test]
        public async Task GetRoomById_ReturnsOk()
        {
            var mockCommand = new Mock<IRoomCommandService>();
            var mockQuery = new Mock<IRoomQueryService>();

            var room = new Room(1, 1, ERoomState.LIBRE);

            mockQuery.Setup(s => s.Handle(It.Is<GetRoomByIdQuery>(q => q.Id == 10)))
                     .ReturnsAsync(room);

            var controller = new RoomsController(mockCommand.Object, mockQuery.Object);

            var result = await controller.RoomById(10);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        

        // ✅ Test 5: Obtener todas las habitaciones por hotel
        [Test]
        public async Task GetAllRooms_ReturnsOk()
        {
            var mockCommand = new Mock<IRoomCommandService>();
            var mockQuery = new Mock<IRoomQueryService>();

            var rooms = new List<Room>
            {
                new Room(1, 1, ERoomState.LIBRE),
                new Room(1, 1, ERoomState.OCUPADO)
            };

            mockQuery.Setup(s => s.Handle(It.Is<GetAllRoomsQuery>(q => q.HotelId == 1))).ReturnsAsync(rooms);

            var controller = new RoomsController(mockCommand.Object, mockQuery.Object);

            var result = await controller.AllRooms(1);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }
    }
}
