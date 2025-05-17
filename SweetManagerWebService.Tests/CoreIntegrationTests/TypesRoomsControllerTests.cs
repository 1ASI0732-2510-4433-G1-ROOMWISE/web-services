using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SweetManagerWebService.Monitoring.Domain.Model.Commands.TypeRoom;
using SweetManagerWebService.Monitoring.Domain.Model.Entities;
using SweetManagerWebService.Monitoring.Domain.Model.Queries.TypeRoom;
using SweetManagerWebService.Monitoring.Domain.Services.TypeRoom;
using SweetManagerWebService.Monitoring.Interfaces.REST;
using SweetManagerWebService.Monitoring.Interfaces.REST.Resources.TypeRoom;

namespace SweetManagerWebService.Tests.CoreIntegrationTests
{
    [TestFixture]
    public class TypesRoomsControllerTests
    {
        // ✅ Test 1: Crear tipo de habitación correctamente
        [Test]
        public async Task CreateTypeRoom_ReturnsOk()
        {
            var mockCommand = new Mock<ITypeRoomCommandService>();
            var mockQuery = new Mock<ITypeRoomQueryService>();

            var resource = new CreateTypeRoomResource("Suite", 150m);

            mockCommand.Setup(s => s.Handle(It.IsAny<CreateTypeRoomCommand>())).ReturnsAsync(true);

            var controller = new TypesRoomsController(mockCommand.Object, mockQuery.Object);

            var result = await controller.CreateTypeRoom(resource);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(((OkObjectResult)result).Value, Is.EqualTo(true));
        }

        // ❌ Test 2: Fallo intencional - chequeo con valor incorrecto
        [Test]
        public async Task CreateTypeRoom_Fail_BadResponseValue()
        {
            var mockCommand = new Mock<ITypeRoomCommandService>();
            var mockQuery = new Mock<ITypeRoomQueryService>();

            var resource = new CreateTypeRoomResource("Suite", 150m);

            mockCommand.Setup(s => s.Handle(It.IsAny<CreateTypeRoomCommand>())).ReturnsAsync(true);

            var controller = new TypesRoomsController(mockCommand.Object, mockQuery.Object);

            var result = await controller.CreateTypeRoom(resource);

            Assert.That(((OkObjectResult)result).Value, Is.EqualTo("Tipo creado")); // ❌ falla esperada
        }

        // ✅ Test 3: Obtener tipo de habitación por ID
        [Test]
        public async Task GetTypeRoomById_ReturnsOk()
        {
            var mockCommand = new Mock<ITypeRoomCommandService>();
            var mockQuery = new Mock<ITypeRoomQueryService>();

            var typeRoom = new TypeRoom("Doble", 100m);

            mockQuery.Setup(s => s.Handle(It.Is<GetTypeRoomByIdQuery>(q => q.Id == 5))).ReturnsAsync(typeRoom);

            var controller = new TypesRoomsController(mockCommand.Object, mockQuery.Object);

            var result = await controller.TypeRoomById(5);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        // ❌ Test 4: Fallo por no encontrar tipo habitación
        [Test]
        public async Task GetTypeRoomById_NotFound_Fail_WrongResultType()
        {
            var mockCommand = new Mock<ITypeRoomCommandService>();
            var mockQuery = new Mock<ITypeRoomQueryService>();

            mockQuery.Setup(s => s.Handle(It.IsAny<GetTypeRoomByIdQuery>())).ReturnsAsync((TypeRoom)null);

            var controller = new TypesRoomsController(mockCommand.Object, mockQuery.Object);

            var result = await controller.TypeRoomById(99);

            // Esto fallará porque el resultado real es BadRequestResult, no OkObjectResult
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        // ✅ Test 5: Obtener todos los tipos de habitación por hotel
        [Test]
        public async Task GetAllTypesRooms_ReturnsOk()
        {
            var mockCommand = new Mock<ITypeRoomCommandService>();
            var mockQuery = new Mock<ITypeRoomQueryService>();

            var types = new List<TypeRoom>
            {
                new TypeRoom("Simple", 50m),
                new TypeRoom("Doble", 100m)
            };

            mockQuery.Setup(s => s.Handle(It.Is<GetAllTypesRoomsQuery>(q => q.HotelId == 1))).ReturnsAsync(types);

            var controller = new TypesRoomsController(mockCommand.Object, mockQuery.Object);

            var result = await controller.AllTypesRooms(1);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }
    }
}
