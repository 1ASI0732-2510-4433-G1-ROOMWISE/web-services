using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.Profiles.Domain.Model.Commands.Customer;
using SweetManagerWebService.Profiles.Domain.Repositories;
using SweetManagerWebService.Profiles.Domain.Services.Customer;

namespace SweetManagerWebService.Profiles.Application.Internal.CommandService;

// Service class to handle customer-related commands
public class CustomerCommandService(ICustomerRepository customerRepository, IUnitOfWork unitOfWork): ICustomerCommandService
{
    // Method to handle the creation of a new customer
    public async Task<bool> Handle(CreateCustomerCommand command)
    {
        try
        {
            // Adds the new customer to the repository asynchronously
            await customerRepository.AddAsync(new(command));
            
            // Completes the unit of work to persist changes to the database
            await unitOfWork.CompleteAsync();
            
            // Returns true if the customer was successfully created
            return true;
        }
        catch (Exception)
        {
            // Returns false in case of an error
            return false;
        }
    }

    // Method to handle updating a customer's details (such as email, phone, and state)
    public async Task<bool> Handle(UpdateCustomerCommand command) =>
        // Updates the customer's state in the repository asynchronously
        await customerRepository.UpdateCustomerStateAsync(
            command.Id, command.Email, command.Phone, command.State);
}
