namespace SweetManagerWebService.Communication.Interfaces.REST.Resources.Notification;

public record NotificationResource(int Id, int TypesNotificationsId, int? OwnersId, int? AdminsId, int? WorkersId, string Title, string Description, DateTimeOffset? StartDate);