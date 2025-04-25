using SweetManagerWebService.IAM.Domain.Model.Entities.Credentials;
using SweetManagerWebService.IAM.Domain.Model.Queries;

namespace SweetManagerWebService.IAM.Domain.Services.Credential.Admin;

public interface IAdminCredentialQueryService
{
    Task<AdminCredential?> Handle(GetUserCredentialByIdQuery query);
    
}