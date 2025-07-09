using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.Communication.Domain.Model.Aggregates;
using SweetManagerWebService.Communication.Domain.Model.Commands;
using SweetManagerWebService.Communication.Domain.Repositories;
using SweetManagerWebService.Communication.Domain.Services.Notification;

namespace SweetManagerWebService.Communication.Application.CommandService;
// Este servicio maneja la creación de notificaciones para diferentes tipos de usuarios (Admins, Workers, Owners).
// Se verifica si los IDs son 0 para tratarlos como null (es decir, sin destinatario en ese rol).
// La notificación es almacenada mediante el repositorio y se confirma la transacción con UnitOfWork.


public class NotificationCommandService(INotificationRepository notificationRepository, IUnitOfWork unitOfWork) : INotificationCommandService
{
    public async Task<bool> Handle(CreateNotificationCommand command)
    {
        try
        {
            var adminsId = command.AdminsId;
            if (command.AdminsId is 0)
                adminsId = null;

            var workersId = command.WorkersId;

            if (command.WorkersId is 0)
                workersId = null;

            var ownersId = command.OwnersId;
            if (command.OwnersId is 0)
                ownersId = null;
            
            await notificationRepository.AddAsync(new Notification
            {
                TypesNotificationsId = command.TypesNotificationsId,
                OwnersId = ownersId,
                AdminsId = adminsId,
                WorkersId = workersId,
                Title = command.Title,
                Description = command.Description
            });

            await unitOfWork.CompleteAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
