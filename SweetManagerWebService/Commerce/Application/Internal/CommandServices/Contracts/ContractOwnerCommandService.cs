using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.Commerce.Domain.Model.Commands.Contracts;
using SweetManagerWebService.Commerce.Domain.Model.Entities.Contracts;
using SweetManagerWebService.Commerce.Domain.Repositories.Contracts;
using SweetManagerWebService.Commerce.Domain.Services.Contracts;

namespace SweetManagerWebService.Commerce.Application.Internal.CommandServices.Contracts;

public class ContractOwnerCommandService(IContractOwnerRepository contractOwnerRepository,
    IUnitOfWork unitOfWork) : IContractOwnerCommandService
{
    public async Task<bool> Handle(CreateContractOwnerCommand command)
    {
        try
        {
            await contractOwnerRepository.AddAsync(
                new ContractOwner(command.SubscriptionId, command.OwnersId, command.State));

            await unitOfWork.CompleteAsync();
            
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}