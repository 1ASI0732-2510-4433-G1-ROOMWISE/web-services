using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SweetManagerWebService.Communication.Domain.Model.Aggregates;
using SweetManagerWebService.Communication.Domain.Model.Commands;
using SweetManagerWebService.Communication.Domain.Model.Queries.Notificacion;
using SweetManagerWebService.Communication.Domain.Services.Notification;
using SweetManagerWebService.Communication.Interfaces.REST;
using SweetManagerWebService.Communication.Interfaces.REST.Resources.Notification;


namespace SweetManagerWebService.Tests.CoreIntegrationTests;

[TestFixture]
public class NotificationsControllerTests
{
    // ✅ 1. Crear notificación correctamente
    [Test]
    public async Task CreateNotification_ReturnsOk()
    {
        var mockCommand = new Mock<INotificationCommandService>();
        var mockQuery = new Mock<INotificationQueryService>();
        var controller = new NotificationsController(mockCommand.Object, mockQuery.Object);

        var resource = new CreateNotificationResource(1, null, 1, null, "Title", "Message");

        mockCommand.Setup(s => s.Handle(It.IsAny<CreateNotificationCommand>())).ReturnsAsync(true);

        var result = await controller.CreateNotification(resource);

        Assert.That(result, Is.TypeOf<OkObjectResult>());
        Assert.That(((OkObjectResult)result).Value, Is.EqualTo(true));
    }

    

    // ✅ 3. Obtener todas las notificaciones por hotel
    [Test]
    public async Task AllNotifications_ReturnsList()
    {
        var mockCommand = new Mock<INotificationCommandService>();
        var mockQuery = new Mock<INotificationQueryService>();
        var controller = new NotificationsController(mockCommand.Object, mockQuery.Object);

        var notifs = new List<Notification>
        {
            new Notification { Title = "A", Description = "X", TypesNotificationsId = 1 },
            new Notification { Title = "B", Description = "Y", TypesNotificationsId = 2 }
        };

        mockQuery.Setup(q => q.Handle(It.IsAny<GetAllNotificationsQuery>()))
                 .ReturnsAsync(notifs);

        var result = await controller.AllNotifications(1);

        Assert.That(result, Is.TypeOf<OkObjectResult>());
        var list = ((OkObjectResult)result).Value as IEnumerable<NotificationResource>;
        Assert.That(list.Count(), Is.EqualTo(2));
    }

    // ✅ 4. Notificación por ID inválido debe retornar BadRequest
    [Test]
    public async Task NotificationById_InvalidId_ReturnsBadRequest()
    {
        var controller = new NotificationsController(new Mock<INotificationCommandService>().Object, new Mock<INotificationQueryService>().Object);
        var result = await controller.NotificationById(0);

        Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        Assert.That(((BadRequestObjectResult)result).Value, Is.EqualTo("Invalid Id"));
    }

    
}
