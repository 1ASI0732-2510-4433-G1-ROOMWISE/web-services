using SweetManagerWebService.Communication.Domain.Model.Queries.Notificacion;

namespace SweetManagerWebService.Communication.Domain.Services.Notification;

public interface INotificationQueryService
{
    Task<IEnumerable<Model.Aggregates.Notification>> Handle(GetAllNotificationsQuery query);

    Task<Model.Aggregates.Notification?> Handle(GetNotificationByIdQuery query);

    Task<IEnumerable<Model.Aggregates.Notification>> Handle(GetAllNotificationsByWorkerIdQuery query);

    Task<IEnumerable<Model.Aggregates.Notification>> Handle(GetAllNotificationsByHotelIdAndExistOwnersIdQuery query);
    
}