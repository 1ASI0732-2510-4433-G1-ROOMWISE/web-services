using SweetManagerWebService.Communication.Domain.Model.Aggregates;
using SweetManagerWebService.Communication.Domain.Model.Commands;

namespace SweetManagerWebService.Tests.CoreEntitiesUnitTests;

[TestFixture]
public class NotificationTests
{
    [Test]
    public void Notification_Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var typeNotificationId = 1;
        var ownerId = 2;
        var adminId = null as int?;
        var workerId = null as int?;
        var title = "Promoción Especial";
        var description = "Disfruta de nuestra promoción de temporada";
        
        // Act
        var notification = new Notification
        {
            TypesNotificationsId = typeNotificationId,
            OwnersId = ownerId,
            AdminsId = adminId,
            WorkersId = workerId,
            Title = title,
            Description = description
        };
        
        // Assert
        Assert.That(notification.TypesNotificationsId, Is.EqualTo(typeNotificationId));
        Assert.That(notification.OwnersId, Is.EqualTo(ownerId));
        Assert.That(notification.AdminsId, Is.EqualTo(adminId));
        Assert.That(notification.WorkersId, Is.EqualTo(workerId));
        Assert.That(notification.Title, Is.EqualTo(title));
        Assert.That(notification.Description, Is.EqualTo(description));
    }
    
    [Test]
    public void Notification_WithCreateCommand_ShouldInitializeProperties()
    {
        // Arrange
        var command = new CreateNotificationCommand(
            TypesNotificationsId: 2,
            OwnersId: null,
            AdminsId: 3,
            WorkersId: null,
            Title: "Actualización del Sistema",
            Description: "Hemos actualizado el sistema con nuevas funciones"
        );
        
        // Act
        var notification = new Notification
        {
            TypesNotificationsId = command.TypesNotificationsId,
            OwnersId = command.OwnersId,
            AdminsId = command.AdminsId,
            WorkersId = command.WorkersId,
            Title = command.Title,
            Description = command.Description
        };
        
        // Assert
        Assert.That(notification.TypesNotificationsId, Is.EqualTo(command.TypesNotificationsId));
        Assert.That(notification.OwnersId, Is.EqualTo(command.OwnersId));
        Assert.That(notification.AdminsId, Is.EqualTo(command.AdminsId));
        Assert.That(notification.WorkersId, Is.EqualTo(command.WorkersId));
        Assert.That(notification.Title, Is.EqualTo(command.Title));
        Assert.That(notification.Description, Is.EqualTo(command.Description));
    }
    
    [Test]
    public void Notification_TitleProperty_ShouldFailWithIncorrectValue()
    {
        // Arrange
        var typeNotificationId = 2;
        var workerId = 10;
        var title = "Recordatorio de Tarea";
        var description = "No olvides completar tus tareas pendientes";

        // Act
        var notification = new Notification
        {
            TypesNotificationsId = typeNotificationId,
            WorkersId = workerId,
            OwnersId = null,
            AdminsId = null,
            Title = title,
            Description = description
        };

        // Assert - Esta prueba fallará intencionalmente
        Assert.That(notification.Title, Is.EqualTo("Título incorrecto"));
        // La prueba fallará porque estamos comparando con un título diferente al que asignamos
    }
    
}