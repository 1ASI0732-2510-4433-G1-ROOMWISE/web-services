using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.Communication.Domain.Model.Entities;

namespace SweetManagerWebService.Communication.Domain.Repositories
{
    public interface ITypeNotificationRepository: IBaseRepository<TypeNotification>
    {
        Task<bool> FindByNameAsync(string name);
        
    }
}