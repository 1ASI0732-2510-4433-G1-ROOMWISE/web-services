using SweetManagerWebService.IAM.Domain.Model.Entities.Credentials;
using SweetManagerWebService.IAM.Domain.Repositories.Credential;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerWebService.IAM.Infrastructure.Persistence.EFC.Repositories.Credential;

public class AdminCredentialRepository(SweetManagerContext context) : BaseRepository<AdminCredential>(context), IAdminCredentialRepository
{
    public async Task<AdminCredential?> FindByAdminsIdAsync(int adminsId)
        => await Task.Run(() =>
        (
            from ac in Context.Set<AdminCredential>().ToList()
            where ac.AdminsId == adminsId
            select ac
        ).FirstOrDefault());
}