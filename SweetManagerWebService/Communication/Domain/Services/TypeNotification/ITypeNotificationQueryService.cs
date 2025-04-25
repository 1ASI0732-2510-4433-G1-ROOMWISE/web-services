using SweetManagerWebService.Communication.Domain.Model.Queries.TypeNotification;

namespace SweetManagerWebService.Communication.Domain.Services.TypeNotification;

public interface ITypeNotificationQueryService
{
    Task<IEnumerable<Model.Entities.TypeNotification>> Handle(GetAllTypesNotificationsQuery query);

    Task<Model.Entities.TypeNotification?> Handle(GetTypeNotificationByIdQuery query);
}