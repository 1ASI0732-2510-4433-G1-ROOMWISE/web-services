using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.Communication.Domain.Model.Aggregates;

namespace SweetManagerWebService.Communication.Domain.Repositories;

public interface INotificationRepository : IBaseRepository<Notification>
{
    Task<IEnumerable<Notification>> FindByTypeNotificationIdAsync(int typeNotificationId);

    Task<IEnumerable<Notification>> FindAllByHotelIdAsync(int hotelId);

    Task<IEnumerable<Notification>> FindAllByWorkerIdAsync(int workerId);

    Task<IEnumerable<Notification>> FindAllByHotelIdAndExitsOwnersIdAsync(int hotelId);
    
}