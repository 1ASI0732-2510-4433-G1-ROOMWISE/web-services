using SweetManagerWebService.IAM.Domain.Model.Entities.Credentials;
using SweetManagerWebService.IAM.Domain.Model.Queries;

namespace SweetManagerWebService.IAM.Domain.Services.Credential.Worker;

public interface IWorkerCredentialQueryService
{
    Task<WorkerCredential?> Handle(GetUserCredentialByIdQuery query);
    
}