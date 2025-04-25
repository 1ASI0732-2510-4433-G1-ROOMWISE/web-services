using SweetManagerWebService.Profiles.Domain.Model.Aggregates;
using SweetManagerWebService.Profiles.Domain.Model.Commands;
using SweetManagerWebService.Profiles.Domain.Model.Commands.Customer;

namespace SweetManagerWebService.Profiles.Domain.Services.Customer;

public interface ICustomerCommandService
{
    Task<bool> Handle(CreateCustomerCommand command);
    
    Task<bool> Handle(UpdateCustomerCommand command);
}