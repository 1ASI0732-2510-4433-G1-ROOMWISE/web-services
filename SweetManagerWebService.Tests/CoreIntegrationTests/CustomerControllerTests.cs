using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SweetManagerWebService.Profiles.Domain.Model.Commands.Customer;
using SweetManagerWebService.Profiles.Domain.Model.Queries.Customer;
using SweetManagerWebService.Profiles.Domain.Model.Aggregates;
using SweetManagerWebService.Profiles.Domain.Services.Customer;
using SweetManagerWebService.Profiles.Interfaces.REST;
using SweetManagerWebService.Profiles.Interfaces.REST.Resources.Customer;

namespace SweetManagerWebService.Tests.CoreIntegrationTests;

[TestFixture]
public class CustomerControllerTests
{
    // ✅ Test 1: Crear cliente exitosamente
    [Test]
    public async Task CreateCustomer_ReturnsOk()
    {
        var mockCommand = new Mock<ICustomerCommandService>();
        var mockQuery = new Mock<ICustomerQueryService>();

        var resource = new CreateCustomerResource(1, "user123", "John", "Doe", "john@example.com", 123456789, "active");

        mockCommand.Setup(s => s.Handle(It.IsAny<CreateCustomerCommand>())).ReturnsAsync(true);

        var controller = new CustomerController(mockCommand.Object, mockQuery.Object);

        var result = await controller.CreateCustomer(resource);

        Assert.That(result, Is.TypeOf<OkObjectResult>());
        Assert.That(((OkObjectResult)result).Value, Is.True);
    }

    // ✅ Test 2: Crear cliente falla (retorna BadRequest)
    [Test]
    public async Task CreateCustomer_WhenFails_ReturnsBadRequest()
    {
        var mockCommand = new Mock<ICustomerCommandService>();
        var mockQuery = new Mock<ICustomerQueryService>();

        var resource = new CreateCustomerResource(1, "user123", "John", "Doe", "john@example.com", 123456789, "active");

        mockCommand.Setup(s => s.Handle(It.IsAny<CreateCustomerCommand>())).ReturnsAsync(false);

        var controller = new CustomerController(mockCommand.Object, mockQuery.Object);

        var result = await controller.CreateCustomer(resource);

        Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        Assert.That(((BadRequestObjectResult)result).Value, Is.EqualTo("Failed to create customer."));
    }

    // ✅ Test 3: Actualizar cliente correctamente
    [Test]
    public async Task UpdateCustomer_ReturnsOk()
    {
        var mockCommand = new Mock<ICustomerCommandService>();
        var mockQuery = new Mock<ICustomerQueryService>();

        var resource = new UpdateCustomerResource(1, "john.doe@example.com", 987654321, "inactive");

        mockCommand.Setup(s => s.Handle(It.IsAny<UpdateCustomerCommand>())).ReturnsAsync(true);

        var controller = new CustomerController(mockCommand.Object, mockQuery.Object);

        var result = await controller.UpdateCustomer(resource);

        Assert.That(result, Is.TypeOf<OkObjectResult>());
        Assert.That(((OkObjectResult)result).Value, Is.True);
    }

    // ✅ Test 4: Actualizar cliente falla (retorna BadRequest)
    [Test]
    public async Task UpdateCustomer_WhenFails_ReturnsBadRequest()
    {
        var mockCommand = new Mock<ICustomerCommandService>();
        var mockQuery = new Mock<ICustomerQueryService>();

        var resource = new UpdateCustomerResource(1, "fail@example.com", 999999999, "inactive");

        mockCommand.Setup(s => s.Handle(It.IsAny<UpdateCustomerCommand>())).ReturnsAsync(false);

        var controller = new CustomerController(mockCommand.Object, mockQuery.Object);

        var result = await controller.UpdateCustomer(resource);

        Assert.That(result, Is.TypeOf<BadRequestResult>());
    }

    // ✅ Test 5: Obtener clientes por hotel ID
    [Test]
    public async Task AllCustomers_ReturnsCustomerList()
    {
        var mockCommand = new Mock<ICustomerCommandService>();
        var mockQuery = new Mock<ICustomerQueryService>();

        var customers = new List<Customer>
        {
            new Customer("user123", "Jane", "Doe", "jane@example.com", 123456789, "active") { Id = 1 },
            new Customer("user456", "John", "Smith", "john@example.com", 987654321, "active") { Id = 2 }
        };

        mockQuery.Setup(q => q.Handle(It.Is<GetAllCustomersQuery>(c => c.HotelId == 1)))
                 .ReturnsAsync(customers);

        var controller = new CustomerController(mockCommand.Object, mockQuery.Object);

        var result = await controller.AllCustomers(1);

        Assert.That(result, Is.TypeOf<OkObjectResult>());
        var list = ((OkObjectResult)result).Value as IEnumerable<CustomerResource>;
        Assert.That(list, Is.Not.Null);
        Assert.That(list.Count(), Is.EqualTo(2));
    }
}
