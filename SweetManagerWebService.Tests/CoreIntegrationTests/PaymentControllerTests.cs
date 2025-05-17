using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SweetManagerWebService.Commerce.Domain.Model.Commands.Payments;
using SweetManagerWebService.Commerce.Domain.Model.Entities.Payments;
using SweetManagerWebService.Commerce.Domain.Model.Queries.Payments;
using SweetManagerWebService.Commerce.Domain.Services.Payments;
using SweetManagerWebService.Commerce.Interfaces.REST;
using SweetManagerWebService.Commerce.Interfaces.REST.Resources.Payments;

namespace SweetManagerWebService.Tests.CoreIntegrationTests;

[TestFixture]
public class PaymentControllerTests
{
    // ✅ 1. Éxito: Crear pago del dueño correctamente
    [Test]
    public async Task CreatePaymentOwner_ReturnsOk()
    {
        var mockCommandOwner = new Mock<IPaymentOwnerCommandService>();
        var controller = CreateController(mockCommandOwner: mockCommandOwner);

        var resource = new CreatePaymentOwnerResource(1, "Test", 150.0m);

        mockCommandOwner
            .Setup(s => s.Handle(It.IsAny<CreatePaymentOwnerCommand>()))
            .ReturnsAsync(true);

        var result = await controller.CreatePaymentOwner(resource);

        Assert.That(result, Is.TypeOf<OkObjectResult>());
        Assert.That(((OkObjectResult)result).Value, Is.EqualTo("Payment registered correctly!"));
    }

    // ❌ 2. Falla esperada: Asserts incorrectos intencionalmente (tipo incorrecto)
    [Test]
    public async Task CreatePaymentOwner_IntentionalFail_WrongType()
    {
        var mockCommandOwner = new Mock<IPaymentOwnerCommandService>();
        var controller = CreateController(mockCommandOwner: mockCommandOwner);

        var resource = new CreatePaymentOwnerResource(1, "This will fail", 100.0m);

        mockCommandOwner
            .Setup(s => s.Handle(It.IsAny<CreatePaymentOwnerCommand>()))
            .ReturnsAsync(true);

        var result = await controller.CreatePaymentOwner(resource);

        // ❌ Esto fallará porque el resultado NO es BadRequest
        Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
    }

    // ✅ 3. Lista de pagos por dueño
    [Test]
    public async Task GetPaymentsByOwnerId_ReturnsList()
    {
        var mockQueryOwner = new Mock<IPaymentOwnerQueryService>();
        var controller = CreateController(mockQueryOwner: mockQueryOwner);

        mockQueryOwner
            .Setup(s => s.Handle(It.IsAny<GetAllPaymentOwnersByOwnerIdQuery>()))
            .ReturnsAsync(new List<PaymentOwner>
            {
                new PaymentOwner(1, "Servicio", 90.0m)
            });

        var result = await controller.GetPaymentsByOwnerId(1);

        Assert.That(result, Is.TypeOf<OkObjectResult>());
        var list = ((OkObjectResult)result).Value as IEnumerable<PaymentOwnerResource>;
        Assert.That(list.Any(), Is.True);
    }

    // ❌ 4. Falla esperada: Lista vacía pero esperamos que tenga elementos
    [Test]
    public async Task GetPaymentsByCustomerId_WhenEmpty_IntentionalFail()
    {
        var mockQueryCustomer = new Mock<IPaymentCustomerQueryService>();
        var controller = CreateController(mockQueryCustomer: mockQueryCustomer);

        mockQueryCustomer
            .Setup(s => s.Handle(It.IsAny<GetAllPaymentCustomersByCustomerIdQuery>()))
            .ReturnsAsync(new List<PaymentCustomer>()); // Lista vacía

        var result = await controller.GetPaymentsByCustomerId(123);

        Assert.That(result, Is.TypeOf<OkObjectResult>());
        var list = ((OkObjectResult)result).Value as IEnumerable<PaymentCustomerResource>;

        // ❌ Esto fallará porque la lista está vacía
        Assert.That(list.Any(), Is.True);
    }

    // ✅ 5. Excepción controlada al obtener ingresos comparativos
    [Test]
    public async Task GetComparativeIncomesAndExpenses_WhenException_ReturnsBadRequest()
    {
        var mockDashboard = new Mock<IDashboardQueryService>();
        var controller = CreateController(mockDashboard: mockDashboard);

        mockDashboard
            .Setup(s => s.Handle(It.IsAny<SweetManagerWebService.Commerce.Domain.Model.Queries.Dashboard.GetAdministrationWeeklyExpensesByHotelId>()))
            .ThrowsAsync(new Exception("DB error"));

        var result = await controller.GetComparativeIncomesAndExpenses(10);

        Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        Assert.That(((BadRequestObjectResult)result).Value, Is.EqualTo("DB error"));
    }
    
    private PaymentController CreateController(
        Mock<IPaymentOwnerCommandService>? mockCommandOwner = null,
        Mock<IPaymentCustomerCommandService>? mockCommandCustomer = null,
        Mock<IPaymentOwnerQueryService>? mockQueryOwner = null,
        Mock<IPaymentCustomerQueryService>? mockQueryCustomer = null,
        Mock<IDashboardQueryService>? mockDashboard = null
    )
    {
        return new PaymentController(
            mockCommandOwner?.Object ?? new Mock<IPaymentOwnerCommandService>().Object,
            mockCommandCustomer?.Object ?? new Mock<IPaymentCustomerCommandService>().Object,
            mockQueryOwner?.Object ?? new Mock<IPaymentOwnerQueryService>().Object,
            mockQueryCustomer?.Object ?? new Mock<IPaymentCustomerQueryService>().Object,
            mockDashboard?.Object ?? new Mock<IDashboardQueryService>().Object
        );
    }
}
