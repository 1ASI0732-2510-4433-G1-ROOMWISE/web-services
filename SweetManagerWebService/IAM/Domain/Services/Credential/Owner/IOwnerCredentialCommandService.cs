using SweetManagerWebService.IAM.Domain.Model.Commands.Authentication.Credential;

namespace SweetManagerWebService.IAM.Domain.Services.Credential.Owner;

public interface IOwnerCredentialCommandService
{
    Task<bool> Handle(CreateUserCredentialCommand command);
}