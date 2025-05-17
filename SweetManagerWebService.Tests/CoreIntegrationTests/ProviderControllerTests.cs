using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SweetManagerWebService.Profiles.Domain.Model.Queries.Provider;
using SweetManagerWebService.Profiles.Domain.Model.Aggregates;
using SweetManagerWebService.Profiles.Domain.Model.Commands.Provider;
using SweetManagerWebService.Profiles.Domain.Services.Provider;
using SweetManagerWebService.Profiles.Interfaces.REST;
using SweetManagerWebService.Profiles.Interfaces.REST.Resources.Provider;


namespace SweetManagerWebService.Tests.CoreIntegrationTests;

[TestFixture]
public class ProviderControllerTests
{
    // ✅ Test 1: Crear proveedor exitosamente
    [Test]
    public async Task CreateProvider_ReturnsOk()
    {
        var mockCommand = new Mock<IProviderCommandService>();
        var mockQuery = new Mock<IProviderQueryService>();

        var resource = new CreateProviderResource(1, "Proveedor Uno", "Calle Falsa", "proveedor@uno.com", 123456789, "active");

        mockCommand.Setup(s => s.Handle(It.IsAny<CreateProviderCommand>())).ReturnsAsync(true);

        var controller = new ProviderController(mockCommand.Object, mockQuery.Object);

        var result = await controller.CreateProvider(resource);

        Assert.That(result, Is.TypeOf<OkObjectResult>());
        Assert.That(((OkObjectResult)result).Value, Is.True);
    }

    // ✅ Test 2: Falla al crear proveedor (retorna BadRequest)
    [Test]
    public async Task CreateProvider_WhenFails_ReturnsBadRequest()
    {
        var mockCommand = new Mock<IProviderCommandService>();
        var mockQuery = new Mock<IProviderQueryService>();

        var resource = new CreateProviderResource(1, "ErrorProv", "Calle Error", "error@prov.com", 111, "inactive");

        mockCommand.Setup(s => s.Handle(It.IsAny<CreateProviderCommand>())).ThrowsAsync(new Exception("Error interno"));

        var controller = new ProviderController(mockCommand.Object, mockQuery.Object);

        var result = await controller.CreateProvider(resource);

        Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        Assert.That(((BadRequestObjectResult)result).Value, Is.EqualTo("Error interno"));
    }

    // ✅ Test 3: Actualizar proveedor correctamente
    [Test]
    public async Task UpdateProvider_ReturnsOk()
    {
        var mockCommand = new Mock<IProviderCommandService>();
        var mockQuery = new Mock<IProviderQueryService>();

        var resource = new UpdateProviderResource(1, "Nueva Dir", "nuevo@prov.com", 987654321, "active");

        mockCommand.Setup(s => s.Handle(It.IsAny<UpdateProviderCommand>())).ReturnsAsync(true);

        var controller = new ProviderController(mockCommand.Object, mockQuery.Object);

        var result = await controller.UpdateProvider(resource);

        Assert.That(result, Is.TypeOf<OkObjectResult>());
        Assert.That(((OkObjectResult)result).Value, Is.True);
    }

    // ✅ Test 4: Falla al actualizar proveedor
    [Test]
    public async Task UpdateProvider_WhenFails_ReturnsBadRequest()
    {
        var mockCommand = new Mock<IProviderCommandService>();
        var mockQuery = new Mock<IProviderQueryService>();

        var resource = new UpdateProviderResource(1, "Err Dir", "err@prov.com", 111111111, "inactive");

        mockCommand.Setup(s => s.Handle(It.IsAny<UpdateProviderCommand>())).ReturnsAsync(false);

        var controller = new ProviderController(mockCommand.Object, mockQuery.Object);

        var result = await controller.UpdateProvider(resource);

        Assert.That(result, Is.TypeOf<BadRequestResult>());
    }

    // ✅ Test 5: Obtener proveedores por HotelId
    [Test]
    public async Task AllProviders_ReturnsList()
    {
        var mockCommand = new Mock<IProviderCommandService>();
        var mockQuery = new Mock<IProviderQueryService>();

        var providers = new List<Provider>
        {
            new Provider("Prov1", "Dir1", "prov1@mail.com", 1234, "active") { Id = 1 },
            new Provider("Prov2", "Dir2", "prov2@mail.com", 5678, "active") { Id = 2 }
        };

        mockQuery.Setup(q => q.Handle(It.Is<GetAllProvidersQuery>(q => q.HotelId == 99)))
                 .ReturnsAsync(providers);

        var controller = new ProviderController(mockCommand.Object, mockQuery.Object);

        var result = await controller.AllProviders(99);

        Assert.That(result, Is.TypeOf<OkObjectResult>());
        var list = ((OkObjectResult)result).Value as IEnumerable<ProviderResource>;
        Assert.That(list.Count(), Is.EqualTo(2));
    }
}
