using SweetManagerWebService.IAM.Domain.Model.Entities.Credentials;
using SweetManagerWebService.IAM.Domain.Model.Queries;

namespace SweetManagerWebService.IAM.Domain.Services.Credential.Owner;

public interface IOwnerCredentialQueryService
{
    Task<OwnerCredential?> Handle(GetUserCredentialByIdQuery query);
    
}