using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.IAM.Application.Internal.OutboundServices;
using SweetManagerWebService.IAM.Domain.Model.Commands.Authentication.Credential;
using SweetManagerWebService.IAM.Domain.Model.Entities.Credentials;
using SweetManagerWebService.IAM.Domain.Repositories.Credential;
using SweetManagerWebService.IAM.Domain.Services.Credential.Owner;

namespace SweetManagerWebService.IAM.Application.Internal.CommandServices.Credential;

public class OwnerCredentialCommandService(IUnitOfWork unitOfWork, 
    IHashingService hashingService, IOwnerCredentialRepository ownerCredentialRepository) : IOwnerCredentialCommandService
{
    public async Task<bool> Handle(CreateUserCredentialCommand command)
    {
        try
        {
            var salt = hashingService.CreateSalt();

            var code = hashingService.HashCode(command.Code, salt);

            await ownerCredentialRepository.AddAsync(new OwnerCredential(command.UserId, string.Concat(salt, code)));

            await unitOfWork.CompleteAsync();
            
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}