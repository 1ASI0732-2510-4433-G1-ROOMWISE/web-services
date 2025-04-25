using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.IAM.Domain.Model.Entities.Credentials;

namespace SweetManagerWebService.IAM.Domain.Repositories.Credential;

public interface IAdminCredentialRepository : IBaseRepository<AdminCredential>
{
    Task<AdminCredential?> FindByAdminsIdAsync(int adminsId);
}