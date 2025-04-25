using SweetManagerWebService.Profiles.Domain.Model.Commands.Customer;
using SweetManagerWebService.Profiles.Interfaces.REST.Resources.Customer;

namespace SweetManagerWebService.Profiles.Interfaces.REST.Transform.Customer;

public class CreateCustomerCommandFromResourceAssembler
{
    public static CreateCustomerCommand ToCommandFromResource(CreateCustomerResource resource)=>
    new(resource.Id, resource.Username,resource.Name,resource.Surname,resource.Email,resource.Phone,resource.State);
    
}

