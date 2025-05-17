using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SweetManagerWebService.Communication.Domain.Model.Entities;
using SweetManagerWebService.Communication.Domain.Model.Queries.TypeNotification;
using SweetManagerWebService.Communication.Domain.Services.TypeNotification;
using SweetManagerWebService.Communication.Interfaces.REST;

namespace SweetManagerWebService.Tests.CoreIntegrationTests;

[TestFixture]
public class TypesNotificationsControllerTests
{
    // ✅ 1. Obtener todos los tipos correctamente
    [Test]
    public async Task AllTypesNotifications_ReturnsList()
    {
        var mockQuery = new Mock<ITypeNotificationQueryService>();
        var controller = new TypesNotificationsController(mockQuery.Object);

        var types = new List<TypeNotification>
        {
            new TypeNotification("MESSAGE"),
            new TypeNotification("ALERT")
        };

        mockQuery.Setup(q => q.Handle(It.IsAny<GetAllTypesNotificationsQuery>())).ReturnsAsync(types);

        var result = await controller.AllTypesNotifications();

        Assert.That(result, Is.TypeOf<OkObjectResult>());
        var list = ((OkObjectResult)result).Value as IEnumerable<object>;
        Assert.That(list.Count(), Is.EqualTo(2));
    }

    // ✅ 2. Obtener tipo por ID válido
    [Test]
    public async Task GetTypeNotificationById_ReturnsType()
    {
        var mockQuery = new Mock<ITypeNotificationQueryService>();
        var controller = new TypesNotificationsController(mockQuery.Object);

        var type = new TypeNotification("ALERT");

        mockQuery.Setup(q => q.Handle(It.Is<GetTypeNotificationByIdQuery>(q => q.Id == 1)))
                 .ReturnsAsync(type);

        var result = await controller.TypeNotificationById(1);

        Assert.That(result, Is.TypeOf<OkObjectResult>());
    }

    // ❌ 3. Fallo intencional: se espera error pero no lo hay
    [Test]
    public async Task GetTypeNotificationById_IntentionalFail_ExpectError()
    {
        var mockQuery = new Mock<ITypeNotificationQueryService>();
        var controller = new TypesNotificationsController(mockQuery.Object);

        var type = new TypeNotification("ALERT");

        mockQuery.Setup(q => q.Handle(It.IsAny<GetTypeNotificationByIdQuery>()))
                 .ReturnsAsync(type);

        var result = await controller.TypeNotificationById(1);

        // ❌ Esto fallará porque realmente retorna Ok, no BadRequest
        Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
    }

    // ✅ 4. ID inválido devuelve BadRequest
    [Test]
    public async Task GetTypeNotificationById_InvalidId_ReturnsBadRequest()
    {
        var controller = new TypesNotificationsController(new Mock<ITypeNotificationQueryService>().Object);
        var result = await controller.TypeNotificationById(0);

        Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        Assert.That(((BadRequestObjectResult)result).Value, Is.EqualTo("Invalid Id"));
    }

    // ❌ 5. Fallo intencional: se espera nulo pero el mock devuelve entidad
    [Test]
    public async Task GetTypeNotificationById_ExpectNull_ButReturnsEntity()
    {
        var mockQuery = new Mock<ITypeNotificationQueryService>();
        var controller = new TypesNotificationsController(mockQuery.Object);

        mockQuery.Setup(q => q.Handle(It.IsAny<GetTypeNotificationByIdQuery>()))
                 .ReturnsAsync(new TypeNotification("MESSAGE"));

        var result = await controller.TypeNotificationById(999);

        // ❌ Esto fallará porque se esperaba NotFound por nulo, pero hay entidad
        Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
    }
}
