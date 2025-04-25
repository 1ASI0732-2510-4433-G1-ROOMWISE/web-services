using SweetManagerWebService.IAM.Domain.Model.Entities.Credentials;
using SweetManagerWebService.IAM.Domain.Repositories.Credential;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerWebService.IAM.Infrastructure.Persistence.EFC.Repositories.Credential;

public class WorkerCredentialRepository(SweetManagerContext context) : BaseRepository<WorkerCredential>(context), IWorkerCredentialRepository
{
    public async Task<WorkerCredential?> FindByWorkersIdAsync(int workersId)
        => await Task.Run(() => (
            from wc in Context.Set<WorkerCredential>().ToList()
            where wc.WorkersId.Equals(workersId)
            select wc
        ).FirstOrDefault());
}