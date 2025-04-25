using SweetManagerWebService.Communication.Domain.Model.Commands;
using SweetManagerWebService.Communication.Domain.Model.Queries.TypeNotification;
using SweetManagerWebService.Communication.Domain.Services.TypeNotification;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace SweetManagerWebService.Communication.Infrastructure.Population.TypeNotifications;

public class TypeNotificationsInitializer(ITypeNotificationCommandService typeNotificationCommandService,
    ITypeNotificationQueryService typeNotificationQueryService, SweetManagerContext context)
{
    public async Task InitializeAsync()
    {
        // Check if the role table is empty

        var result = await typeNotificationQueryService.Handle(new GetAllTypesNotificationsQuery());

        if (!result.Any())
        {
            // Prepopulate the empty table

            await typeNotificationCommandService.Handle(new SeedTypeNotificationsCommand());
        }
    }
}