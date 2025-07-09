using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.IAM.Domain.Model.Aggregates;
using SweetManagerWebService.IAM.Domain.Model.Entities.Assignments;
using SweetManagerWebService.IAM.Domain.Model.Entities.Roles;
using SweetManagerWebService.IAM.Domain.Repositories.Assignments;
using SweetManagerWebService.Profiles.Domain.Model.Entities;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerWebService.IAM.Infrastructure.Persistence.EFC.Repositories.Assignment;

// Repository class for managing AssignmentWorker entities in the database
public class AssignmentWorkerRepository(SweetManagerContext context) : BaseRepository<AssignmentWorker>(context), IAssignmentWorkerRepository
{
    // Method to find AssignmentWorkers by worker's ID, filtering out past assignments
    public async Task<IEnumerable<AssignmentWorker>> FindByWorkerIdAsync(int workerId)
        => await Task.Run(() => (
            from aw in Context.Set<AssignmentWorker>().ToList()
            join wk in Context.Set<Worker>().ToList() on aw.WorkersId equals wk.Id
            where wk.Id.Equals(workerId) && aw.FinalDate > DateTime.Now
            select aw
        ).ToList());

    // Method to find AssignmentWorkers by admin's ID
    public async Task<IEnumerable<AssignmentWorker>> FindByAdminIdAsync(int adminId)
        => await Task.Run(() => (
            from aw in Context.Set<AssignmentWorker>().ToList()
            where aw.AdminsId.Equals(adminId)
            select aw
        ).ToList());

    // Method to find AssignmentWorkers by worker's area ID
    public async Task<IEnumerable<AssignmentWorker>> FindByWorkerAreaIdAsync(int workerAreaId)
        => await Task.Run(() => (
            from aw in Context.Set<AssignmentWorker>().ToList()
            where aw.WorkersAreasId.Equals(workerAreaId)
            select aw
        ).ToList());
}
