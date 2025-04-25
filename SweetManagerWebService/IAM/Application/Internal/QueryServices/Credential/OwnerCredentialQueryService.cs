using SweetManagerWebService.IAM.Domain.Model.Entities.Credentials;
using SweetManagerWebService.IAM.Domain.Model.Queries;
using SweetManagerWebService.IAM.Domain.Repositories.Credential;
using SweetManagerWebService.IAM.Domain.Services.Credential.Owner;

namespace SweetManagerWebService.IAM.Application.Internal.QueryServices.Credential;

public class OwnerCredentialQueryService(IOwnerCredentialRepository ownerCredentialRepository) : IOwnerCredentialQueryService
{
    public async Task<OwnerCredential?> Handle(GetUserCredentialByIdQuery query)
        => await ownerCredentialRepository.FindByOwnersIdAsync(query.UserId);
}