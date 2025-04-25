using SweetManagerWebService.Profiles.Domain.Model.Commands.Customer;
using SweetManagerWebService.Profiles.Interfaces.REST.Resources.Customer;

namespace SweetManagerWebService.Profiles.Interfaces.REST.Transform.Customer;

public class UpdateCustomerCommandFromResourceAssembler
{
    public static UpdateCustomerCommand ToCommandFromResource(UpdateCustomerResource resource) =>
        new(resource.Id,resource.Email, resource.Phone, resource.State);
}