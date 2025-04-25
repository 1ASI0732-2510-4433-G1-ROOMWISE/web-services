using SweetManagerWebService.IAM.Domain.Model.Entities.Credentials;
using SweetManagerWebService.IAM.Domain.Model.Queries;
using SweetManagerWebService.IAM.Domain.Repositories.Credential;
using SweetManagerWebService.IAM.Domain.Services.Credential.Admin;

namespace SweetManagerWebService.IAM.Application.Internal.QueryServices.Credential;

public class AdminCredentialQueryService(IAdminCredentialRepository adminCredentialRepository): IAdminCredentialQueryService
{
    public async Task<AdminCredential?> Handle(GetUserCredentialByIdQuery query)
        => await adminCredentialRepository.FindByAdminsIdAsync(query.UserId);
}