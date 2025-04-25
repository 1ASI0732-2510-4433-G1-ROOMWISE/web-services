using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.SupplyManagement.Domain.Model.Commands;
using SweetManagerWebService.SupplyManagement.Domain.Model.Exceptions;
using SweetManagerWebService.SupplyManagement.Domain.Repositories;
using SweetManagerWebService.SupplyManagement.Domain.Services;

namespace SweetManagerWebService.SupplyManagement.Application.Internal.Supply;

public class SupplyCommandService(ISupplyRepository supplyRepository, IUnitOfWork unitOfWork) : ISupplyCommandService
{
    ISupplyRepository _supplyRepository = supplyRepository;
    IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<bool> Handle(CreateSupplyCommand command)
    {
        try
        {

            if (string.IsNullOrWhiteSpace(command.Name))
                throw new InvalidSupplyNameException("The name of the supply cannot be empty.");
        
            if (command.Price <= 0)
                throw new InvalidSupplyPriceException("The price of the supply must be greater than zero.");
        
            if (command.Stock < 0)
                throw new InvalidSupplyStockException("The stock of the supply cannot be negative.");

            
            await _supplyRepository.AddAsync(new Domain.Model.Aggregates.Supply(command));
            await _unitOfWork.CompleteAsync();

            return true;
        }
        catch (Exception e)
        {
            
            return false;
        } 
    }


    public async Task<bool> Handle(UpdateSupplyCommand command)
    {
        try
        {
            var existingSupply = await _supplyRepository.FindByIdAsync(command.Id);

            if (existingSupply == null)
                throw new SupplyNotFoundException($"The supply with ID {command.Id} was not found.");

            
            if (string.IsNullOrWhiteSpace(command.Name))
                throw new InvalidSupplyNameException("The name of the supply cannot be empty.");
        
            if (command.Price <= 0)
                throw new InvalidSupplyPriceException("The price of the supply must be greater than zero.");
        
            if (command.Stock < 0)
                throw new InvalidSupplyStockException("The stock of the supply cannot be negative.");

            
            existingSupply.Update(command);
            await _unitOfWork.CompleteAsync();

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }


    public async Task<bool> Handle(DeleteSupplyCommand command)
    {
        try
        {
            var supplyToDelete = await _supplyRepository.FindByIdAsync(command.Id);

            if (supplyToDelete == null)
                throw new SupplyNotFoundException($"The supply with ID {command.Id} was not found.");

            
            _supplyRepository.Remove(supplyToDelete);
            await _unitOfWork.CompleteAsync();

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }


}