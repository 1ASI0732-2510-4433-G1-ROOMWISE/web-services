using SweetManagerWebService.IAM.Domain.Model.Commands.Authentication.Credential;

namespace SweetManagerWebService.IAM.Domain.Services.Credential.Worker;

public interface IWorkerCredentialCommandService
{
    Task<bool> Handle(CreateUserCredentialCommand command);
}