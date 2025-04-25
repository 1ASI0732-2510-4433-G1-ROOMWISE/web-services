using SweetManagerWebService.Communication.Domain.Model.Commands;

namespace SweetManagerWebService.Communication.Domain.Services.Notification;

public interface INotificationCommandService
{
    Task<bool> Handle(CreateNotificationCommand command);
}