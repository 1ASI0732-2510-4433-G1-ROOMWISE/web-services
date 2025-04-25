using SweetManagerWebService.Communication.Interfaces.REST.Resources.Notification;

namespace SweetManagerWebService.Communication.Interfaces.REST.Transform.Notification
{
    public class NotificationResourceFromEntityAssembler
    {
        public static NotificationResource ToResourceFromEntity(Domain.Model.Aggregates.Notification entity) =>
            new(entity.Id, entity.TypesNotificationsId, entity.OwnersId, entity.AdminsId, entity.WorkersId, entity.Title, entity.Description, entity.CreatedDate);
    }
}