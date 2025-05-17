using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SweetManagerWebService.SupplyManagement.Domain.Model.Aggregates;
using SweetManagerWebService.SupplyManagement.Domain.Model.Commands;
using SweetManagerWebService.SupplyManagement.Domain.Model.Queries;
using SweetManagerWebService.SupplyManagement.Domain.Services;
using SweetManagerWebService.SupplyManagement.Interfaces.REST;
using SweetManagerWebService.SupplyManagement.Interfaces.REST.Resources;

namespace SweetManagerWebService.Tests.CoreIntegrationTests;

[TestFixture]
public class SupplyControllerTests
{
    // ✅ Test 1: Crear supply correctamente
    [Test]
    public async Task CreateSupply_ReturnsOk()
    {
        var mockCommand = new Mock<ISupplyCommandService>();
        var mockQuery = new Mock<ISupplyQueryService>();

        var resource = new CreateSupplyResource(1, "Sugar", 10.5m, 100, "active");

        mockCommand.Setup(s => s.Handle(It.IsAny<CreateSupplyCommand>())).ReturnsAsync(true);

        var controller = new SupplyController(mockCommand.Object, mockQuery.Object);

        var result = await controller.CreateSupply(resource);

        Assert.That(result, Is.TypeOf<OkObjectResult>());
        Assert.That(((OkObjectResult)result).Value, Is.True);
    }

    // ✅ Test 2: Actualizar supply correctamente
    [Test]
    public async Task UpdateSupply_ReturnsOk()
    {
        var mockCommand = new Mock<ISupplyCommandService>();
        var mockQuery = new Mock<ISupplyQueryService>();

        var resource = new UpdateSupplyResource(1, 10, "Sugar Updated", 12.0m, 200, "active");

        mockCommand.Setup(s => s.Handle(It.IsAny<UpdateSupplyCommand>())).ReturnsAsync(true);

        var controller = new SupplyController(mockCommand.Object, mockQuery.Object);

        var result = await controller.UpdateSupply(1, resource);

        Assert.That(result, Is.TypeOf<OkObjectResult>());
        Assert.That(((OkObjectResult)result).Value, Is.True);
    }

    // ✅ Test 3: Eliminar supply correctamente
    [Test]
    public async Task DeleteSupply_ReturnsOk()
    {
        var mockCommand = new Mock<ISupplyCommandService>();
        var mockQuery = new Mock<ISupplyQueryService>();

        var resource = new DeleteSupplyResource(1);

        mockCommand.Setup(s => s.Handle(It.IsAny<DeleteSupplyCommand>())).ReturnsAsync(true);

        var controller = new SupplyController(mockCommand.Object, mockQuery.Object);

        var result = await controller.DeleteSupply(resource);

        Assert.That(result, Is.TypeOf<OkObjectResult>());
        Assert.That(((OkObjectResult)result).Value, Is.True);
    }

    // ✅ Test 4: Obtener supply por ID
    [Test]
    public async Task GetSupplyById_ReturnsOk()
    {
        var mockCommand = new Mock<ISupplyCommandService>();
        var mockQuery = new Mock<ISupplyQueryService>();

        var supply = new Supply(1, 2, "Flour", 15.0m, 500, "active");

        mockQuery.Setup(q => q.Handle(It.Is<GetSupplyByIdQuery>(x => x.Id == 1))).ReturnsAsync(supply);

        var controller = new SupplyController(mockCommand.Object, mockQuery.Object);

        var result = await controller.GetSupplyById(1);

        Assert.That(result, Is.TypeOf<OkObjectResult>());
        var returned = ((OkObjectResult)result).Value as SupplyResource;
        Assert.That(returned.Name.ToUpper(), Is.EqualTo("FLOUR"));
    }

    // ✅ Test 5: Obtener supplies por hotelId
    [Test]
    public async Task GetAllSupplies_ReturnsList()
    {
        var mockCommand = new Mock<ISupplyCommandService>();
        var mockQuery = new Mock<ISupplyQueryService>();

        var supplies = new List<Supply>
        {
            new Supply(1, 1, "Salt", 5.5m, 50, "active"),
            new Supply(2, 1, "Oil", 20.0m, 30, "active")
        };

        mockQuery.Setup(q => q.Handle(It.Is<GetAllSuppliesQuery>(q => q.HotelId == 42)))
                 .ReturnsAsync(supplies);

        var controller = new SupplyController(mockCommand.Object, mockQuery.Object);

        var result = await controller.GetAllSupplies(42);

        Assert.That(result, Is.TypeOf<OkObjectResult>());
        var list = ((OkObjectResult)result).Value as IEnumerable<SupplyResource>;
        Assert.That(list.Count(), Is.EqualTo(2));
    }
}
