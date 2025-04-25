using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.Profiles.Domain.Model.Commands.Customer;
using SweetManagerWebService.Profiles.Domain.Repositories;
using SweetManagerWebService.Profiles.Domain.Services.Customer;

namespace SweetManagerWebService.Profiles.Application.Internal.CommandService;

public class CustomerCommandService(ICustomerRepository customerRepository, IUnitOfWork unitOfWork):ICustomerCommandService
{
    public async Task<bool> Handle(CreateCustomerCommand command)
    {
        try
        {
            await customerRepository.AddAsync(new(command));
            await unitOfWork.CompleteAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    
    public async Task<bool> Handle
        (UpdateCustomerCommand command) => 
        await customerRepository.UpdateCustomerStateAsync
        (command.Id,command.Email, command.Phone,command.State);
}