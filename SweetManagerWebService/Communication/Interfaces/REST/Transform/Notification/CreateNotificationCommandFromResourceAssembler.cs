using SweetManagerWebService.Communication.Domain.Model.Commands;
using SweetManagerWebService.Communication.Interfaces.REST.Resources.Notification;

public static class CreateNotificationCommandFromResourceAssembler
{
    public static CreateNotificationCommand ToCommandFromResource(CreateNotificationResource resource)
    {
        return new CreateNotificationCommand(
            resource.TypesNotificationsId,
            resource.OwnersId,
            resource.AdminsId,
            resource.WorkersId,
            resource.Title,
            resource.Description);
    }
}