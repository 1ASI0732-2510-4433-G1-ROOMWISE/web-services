namespace SweetManagerWebService.Communication.Domain.Model.Commands;

public record CreateNotificationCommand(int TypesNotificationsId, int? OwnersId, int? AdminsId, int? WorkersId, string Title, string Description);