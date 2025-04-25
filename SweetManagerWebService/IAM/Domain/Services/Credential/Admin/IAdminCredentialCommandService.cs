using SweetManagerWebService.IAM.Domain.Model.Commands.Authentication.Credential;

namespace SweetManagerWebService.IAM.Domain.Services.Credential.Admin;

public interface IAdminCredentialCommandService
{
    Task<bool> Handle(CreateUserCredentialCommand command);
    
}