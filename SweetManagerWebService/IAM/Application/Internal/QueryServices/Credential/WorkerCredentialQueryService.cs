using SweetManagerWebService.IAM.Domain.Model.Entities.Credentials;
using SweetManagerWebService.IAM.Domain.Model.Queries;
using SweetManagerWebService.IAM.Domain.Repositories.Credential;
using SweetManagerWebService.IAM.Domain.Services.Credential.Worker;

namespace SweetManagerWebService.IAM.Application.Internal.QueryServices.Credential;

public class WorkerCredentialQueryService(IWorkerCredentialRepository workerCredentialRepository) : IWorkerCredentialQueryService
{
    public async Task<WorkerCredential?> Handle(GetUserCredentialByIdQuery query)
        => await workerCredentialRepository.FindByWorkersIdAsync(query.UserId);
}