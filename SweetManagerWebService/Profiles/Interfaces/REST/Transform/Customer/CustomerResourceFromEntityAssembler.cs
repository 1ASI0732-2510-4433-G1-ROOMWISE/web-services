using SweetManagerWebService.Profiles.Interfaces.REST.Resources.Customer;

namespace SweetManagerWebService.Profiles.Interfaces.REST.Transform.Customer;

public class CustomerResourceFromEntityAssembler
{
    public static CustomerResource ToResourceFromEntity(Domain.Model.Aggregates.Customer entity) =>
    new(entity.Id, entity.Username, entity.Name, entity.Surname,entity.Email,entity.Phone,entity.State);

}