using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SweetManagerWebService.Profiles.Domain.Model.Queries.Hotel;
using SweetManagerWebService.Profiles.Domain.Services.Hotel;
using SweetManagerWebService.Profiles.Domain.Model.Entities;
using SweetManagerWebService.Profiles.Interfaces.REST;
using SweetManagerWebService.Profiles.Interfaces.REST.Resources.Hotel;
using SweetManagerWebService.Profiles.Domain.Model.Commands.Hotel;

namespace SweetManagerWebService.Tests.CoreIntegrationTests;

[TestFixture]
public class HotelControllerTests
{
    // ✅ Test 1: Crear hotel correctamente
    [Test]
    public async Task CreateHotel_ReturnsOk()
    {
        var mockCommand = new Mock<IHotelCommandService>();
        var mockQuery = new Mock<IHotelQueryService>();

        var resource = new CreateHotelResource(1, "Hotel Luna", "Bonito hotel", "Av. Central", 987654321, "hotel@luna.com");

        mockCommand.Setup(s => s.Handle(It.IsAny<CreateHotelCommand>())).ReturnsAsync(true);

        var controller = new HotelController(mockCommand.Object, mockQuery.Object);

        var result = await controller.CreateHotel(resource);

        Assert.That(result, Is.TypeOf<OkObjectResult>());
        Assert.That(((OkObjectResult)result).Value, Is.True);
    }

    // ✅ Test 2: Fallo al crear hotel (retorna BadRequest)
    [Test]
    public async Task CreateHotel_WhenFails_ReturnsBadRequest()
    {
        var mockCommand = new Mock<IHotelCommandService>();
        var mockQuery = new Mock<IHotelQueryService>();

        var resource = new CreateHotelResource(1, "Hotel Fallido", "No debe crearse", "Desconocida", 111111111, "fail@hotel.com");

        mockCommand.Setup(s => s.Handle(It.IsAny<CreateHotelCommand>())).ReturnsAsync(false);

        var controller = new HotelController(mockCommand.Object, mockQuery.Object);

        var result = await controller.CreateHotel(resource);

        Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        Assert.That(((BadRequestObjectResult)result).Value, Is.EqualTo("Failed to create hotel"));
    }

    // ✅ Test 3: Actualizar hotel correctamente
    [Test]
    public async Task UpdateHotel_ReturnsOk()
    {
        var mockCommand = new Mock<IHotelCommandService>();
        var mockQuery = new Mock<IHotelQueryService>();

        var resource = new UpdateHotelResource(1, "Hotel Actualizado", 123456789, "nuevo@email.com");

        mockCommand.Setup(s => s.Handle(It.IsAny<UpdateHotelCommand>())).ReturnsAsync(true);

        var controller = new HotelController(mockCommand.Object, mockQuery.Object);

        var result = await controller.UpdateHotel(resource);

        Assert.That(result, Is.TypeOf<OkObjectResult>());
        Assert.That(((OkObjectResult)result).Value, Is.True);
    }

    // ✅ Test 4: Obtener todos los hoteles
    [Test]
    public async Task AllHotels_ReturnsHotelList()
    {
        var mockCommand = new Mock<IHotelCommandService>();
        var mockQuery = new Mock<IHotelQueryService>();

        var hotels = new List<Hotel>
        {
            new Hotel(1, "Hotel Uno", "Desc Uno", "Dir Uno", 1234, "uno@email.com"),
            new Hotel(2, "Hotel Dos", "Desc Dos", "Dir Dos", 5678, "dos@email.com")
        };

        mockQuery.Setup(q => q.Handle(It.IsAny<GetAllHotelsQuery>())).ReturnsAsync(hotels);

        var controller = new HotelController(mockCommand.Object, mockQuery.Object);

        var result = await controller.AllHotels();

        Assert.That(result, Is.TypeOf<OkObjectResult>());
        var list = ((OkObjectResult)result).Value as IEnumerable<HotelResource>;
        Assert.That(list, Is.Not.Null);
        Assert.That(list.Count(), Is.EqualTo(2));
    }

    // ✅ Test 5: Obtener hotel por OwnerId existente
    [Test]
    public async Task HotelsByOwnerId_ReturnsHotel()
    {
        var mockCommand = new Mock<IHotelCommandService>();
        var mockQuery = new Mock<IHotelQueryService>();

        var hotel = new Hotel(1, "Hotel Uno", "Desc", "Dir", 123, "hotel@uno.com");

        mockQuery.Setup(q => q.Handle(It.Is<GetHotelByOwnersIdQuery>(x => x.OwnersId == 1)))
                 .ReturnsAsync(hotel);

        var controller = new HotelController(mockCommand.Object, mockQuery.Object);

        var result = await controller.HotelsByOwnersId(1);

        Assert.That(result, Is.TypeOf<OkObjectResult>());
        var hotelResource = ((OkObjectResult)result).Value as HotelResource;
        Assert.That(hotelResource.Name, Is.EqualTo("HOTEL UNO"));
    }
}
