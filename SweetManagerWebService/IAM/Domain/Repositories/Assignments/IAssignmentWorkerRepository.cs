using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.IAM.Domain.Model.Entities.Assignments;

namespace SweetManagerWebService.IAM.Domain.Repositories.Assignments;

public interface IAssignmentWorkerRepository : IBaseRepository<AssignmentWorker>
{
    
    Task<IEnumerable<AssignmentWorker>> FindByWorkerIdAsync(int workerId);

    Task<IEnumerable<AssignmentWorker>> FindByAdminIdAsync(int adminId);

    Task<IEnumerable<AssignmentWorker>> FindByWorkerAreaIdAsync(int workerAreaId);
    
}