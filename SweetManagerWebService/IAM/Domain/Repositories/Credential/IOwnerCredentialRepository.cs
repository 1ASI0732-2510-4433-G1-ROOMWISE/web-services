using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.IAM.Domain.Model.Entities.Credentials;

namespace SweetManagerWebService.IAM.Domain.Repositories.Credential;

public interface IOwnerCredentialRepository : IBaseRepository<OwnerCredential>
{
    Task<OwnerCredential?> FindByOwnersIdAsync(int ownersId);
}