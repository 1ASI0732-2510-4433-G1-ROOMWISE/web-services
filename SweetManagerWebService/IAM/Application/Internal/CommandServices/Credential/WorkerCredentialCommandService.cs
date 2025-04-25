using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.IAM.Application.Internal.OutboundServices;
using SweetManagerWebService.IAM.Domain.Model.Commands.Authentication.Credential;
using SweetManagerWebService.IAM.Domain.Model.Entities.Credentials;
using SweetManagerWebService.IAM.Domain.Repositories.Credential;
using SweetManagerWebService.IAM.Domain.Services.Credential.Worker;

namespace SweetManagerWebService.IAM.Application.Internal.CommandServices.Credential;

public class WorkerCredentialCommandService(IUnitOfWork unitOfWork, 
    IHashingService hashingService, IWorkerCredentialRepository workerCredentialRepository) : IWorkerCredentialCommandService
{
    public async Task<bool> Handle(CreateUserCredentialCommand command)
    {
        try
        {
            var salt = hashingService.CreateSalt();

            var code = hashingService.HashCode(command.Code, salt);

            await workerCredentialRepository.AddAsync(new WorkerCredential(command.UserId, string.Concat(salt, code)));

            await unitOfWork.CompleteAsync();
            
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}