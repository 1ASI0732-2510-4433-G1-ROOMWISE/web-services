using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.Communication.Domain.Model.Commands;
using SweetManagerWebService.Communication.Domain.Model.Entities;
using SweetManagerWebService.Communication.Domain.Model.ValueObjects;
using SweetManagerWebService.Communication.Domain.Repositories;
using SweetManagerWebService.Communication.Domain.Services.TypeNotification;

namespace SweetManagerWebService.Communication.Application.CommandService;

public class TypeNotificationCommandService(ITypeNotificationRepository typeNotificationRepository,
    IUnitOfWork unitOfWork) : ITypeNotificationCommandService
{
    public async Task<bool> Handle(SeedTypeNotificationsCommand command)
    {
        foreach (var typeNotification in Enum.GetValues(typeof(ETypeNotification)))
        {
            if (await typeNotificationRepository.FindByNameAsync(typeNotification.ToString()!)) continue;
            
            await typeNotificationRepository.AddAsync(new TypeNotification(typeNotification.ToString()!));

            await unitOfWork.CompleteAsync();
        }

        return true;
    }
}