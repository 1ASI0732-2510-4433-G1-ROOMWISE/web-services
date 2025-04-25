using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.IAM.Domain.Model.Entities.Roles;

namespace SweetManagerWebService.IAM.Domain.Repositories.Roles;

public interface IWorkerAreaRepository : IBaseRepository<WorkerArea>
{
    Task<IEnumerable<WorkerArea>> FindAllAsync(int hotelId);

    Task<WorkerArea?> FindByNameAsync(string name, int hotelId);

    Task<int?> FindIdByNameAsync(string name, int hotelId);

    Task<string?> FindByWorkerIdAsync(int workerId);
    
}