using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SweetManagerWebService.SupplyManagement.Domain.Model.Commands;
using SweetManagerWebService.SupplyManagement.Domain.Model.Entities;
using SweetManagerWebService.SupplyManagement.Domain.Model.Queries;
using SweetManagerWebService.SupplyManagement.Domain.Services;
using SweetManagerWebService.SupplyManagement.Interfaces.REST;
using SweetManagerWebService.SupplyManagement.Interfaces.REST.Resources;

namespace SweetManagerWebService.Tests.CoreIntegrationTests;

[TestFixture]
public class SuppliesRequestControllerTests
{
    // ✅ Test 1: Crear solicitud de insumo exitosamente
    [Test]
    public async Task CreateSuppliesRequest_ReturnsOk()
    {
        var mockCommand = new Mock<ISuppliesRequestCommandService>();
        var mockQuery = new Mock<ISuppliesRequestQueryService>();

        var resource = new CreateSuppliesRequestResource(1, 2, 3, 100.5m);

        mockCommand.Setup(s => s.Handle(It.IsAny<CreateSuppliesRequestCommand>())).ReturnsAsync(true);

        var controller = new SuppliesRequestController(mockQuery.Object, mockCommand.Object);

        var result = await controller.CreateSuppliesRequest(resource);

        Assert.That(result, Is.TypeOf<OkObjectResult>());
        Assert.That(((OkObjectResult)result).Value, Is.True);
    }

    // ✅ Test 2: Obtener solicitudes de insumo por HotelId
    [Test]
    public async Task GetAllSuppliesRequest_ReturnsList()
    {
        var mockCommand = new Mock<ISuppliesRequestCommandService>();
        var mockQuery = new Mock<ISuppliesRequestQueryService>();

        var suppliesRequests = new List<SuppliesRequest>
        {
            new SuppliesRequest(1, 1, 1, 2, 100m),
            new SuppliesRequest(2, 2, 2, 3, 150m)
        };

        mockQuery.Setup(q => q.Handle(It.Is<GetAllSuppliesRequestQuery>(x => x.HotelId == 99)))
                 .ReturnsAsync(suppliesRequests);

        var controller = new SuppliesRequestController(mockQuery.Object, mockCommand.Object);

        var result = await controller.GetAllSuppliesRequest(99);

        Assert.That(result, Is.TypeOf<OkObjectResult>());
        var list = ((OkObjectResult)result).Value as IEnumerable<SuppliesRequestResource>;
        Assert.That(list.Count(), Is.EqualTo(2));
    }

    // ✅ Test 3: Obtener solicitud de insumo por ID válido
    [Test]
    public async Task GetSuppliesRequestById_ReturnsOk()
    {
        var mockCommand = new Mock<ISuppliesRequestCommandService>();
        var mockQuery = new Mock<ISuppliesRequestQueryService>();

        var request = new SuppliesRequest(5, 1, 2, 4, 200m);

        mockQuery.Setup(q => q.Handle(It.Is<GetSuppliesRequestByIdQuery>(x => x.Id == 5)))
                 .ReturnsAsync(request);

        var controller = new SuppliesRequestController(mockQuery.Object, mockCommand.Object);

        var result = await controller.GetSuppliesRequestById(5);

        Assert.That(result, Is.TypeOf<OkObjectResult>());
        var resource = ((OkObjectResult)result).Value as SuppliesRequestResource;
        Assert.That(resource.Id, Is.EqualTo(5));
    }

    // ✅ Test 4: Obtener solicitud por PaymentOwnerId existente
    [Test]
    public async Task GetSuppliesRequestByPaymentOwnerId_ReturnsOk()
    {
        var mockCommand = new Mock<ISuppliesRequestCommandService>();
        var mockQuery = new Mock<ISuppliesRequestQueryService>();

        var request = new SuppliesRequest(7, 10, 3, 5, 300m);

        mockQuery.Setup(q => q.Handle(It.Is<GetSuppliesRequestByPaymentOwnerIdQuery>(x => x.PaymentOwnerId == 10)))
                 .ReturnsAsync(request);

        var controller = new SuppliesRequestController(mockQuery.Object, mockCommand.Object);

        var result = await controller.GetSuppliesRequestByPaymentOwnerId(10);

        Assert.That(result, Is.TypeOf<OkObjectResult>());
        var resource = ((OkObjectResult)result).Value as SuppliesRequestResource;
        Assert.That(resource.PaymentsOwnersId, Is.EqualTo(10));
    }

    // ✅ Test 5: Obtener solicitud por SupplyId inexistente (retorna BadRequest)
    [Test]
    public async Task GetSuppliesRequestBySupplyId_NotFound_ReturnsBadRequest()
    {
        var mockCommand = new Mock<ISuppliesRequestCommandService>();
        var mockQuery = new Mock<ISuppliesRequestQueryService>();

        mockQuery.Setup(q => q.Handle(It.Is<GetSuppliesRequestBySupplyIdQuery>(x => x.SupplyId == 999)))
                 .ReturnsAsync((SuppliesRequest?)null);

        var controller = new SuppliesRequestController(mockQuery.Object, mockCommand.Object);

        var result = await controller.GetSuppliesRequestBySupplyId(999);

        Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        var message = ((BadRequestObjectResult)result).Value.ToString();
        Assert.That(message, Does.Contain("No supplies requests found for SupplyId 999"));
    }
}
