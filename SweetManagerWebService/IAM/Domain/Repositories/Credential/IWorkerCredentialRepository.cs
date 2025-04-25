using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.IAM.Domain.Model.Entities.Credentials;

namespace SweetManagerWebService.IAM.Domain.Repositories.Credential;

public interface IWorkerCredentialRepository : IBaseRepository<WorkerCredential>
{
    Task<WorkerCredential?> FindByWorkersIdAsync(int workersId);
    
}