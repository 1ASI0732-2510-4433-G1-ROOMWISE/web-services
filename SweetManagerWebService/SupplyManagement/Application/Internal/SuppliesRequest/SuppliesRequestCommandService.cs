using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.SupplyManagement.Domain.Model.Commands;
using SweetManagerWebService.SupplyManagement.Domain.Model.Exceptions;
using SweetManagerWebService.SupplyManagement.Domain.Repositories;
using SweetManagerWebService.SupplyManagement.Domain.Services;

namespace SweetManagerWebService.SupplyManagement.Application.Internal.SuppliesRequest;

public class SuppliesRequestCommandService(
    ISuppliesRequestRepository suppliesRequestRepository,
    ISupplyRepository supplyRepository,
    IUnitOfWork unitOfWork)
    : ISuppliesRequestCommandService
{
    public async Task<bool> Handle(CreateSuppliesRequestCommand command)
    {
        try
        {

            if (command.Count <= 0)
                throw new InvalidSuppliesRequestCountException("The count must be greater than zero.");
            

            if (command.Amount <= 0)
                throw new InvalidSuppliesRequestAmountException("The amount must be greater than zero.");


            var supply = await supplyRepository.FindByIdAsync(command.SuppliesId);
            if (supply == null)
                throw new SupplyNotFoundException($"The supply with ID {command.SuppliesId} was not found.");

 

            var suppliesRequest = new Domain.Model.Entities.SuppliesRequest(command); 

            await suppliesRequestRepository.AddAsync(suppliesRequest);
            
            await unitOfWork.CompleteAsync();

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}
