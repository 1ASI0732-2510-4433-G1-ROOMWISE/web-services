using SweetManagerWebService.Communication.Domain.Model.Commands;

namespace SweetManagerWebService.Communication.Domain.Services.TypeNotification;

public interface ITypeNotificationCommandService
{
    Task<bool> Handle(SeedTypeNotificationsCommand command);
    
}