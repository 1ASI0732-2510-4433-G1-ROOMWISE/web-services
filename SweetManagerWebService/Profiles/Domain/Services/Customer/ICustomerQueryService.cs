using SweetManagerWebService.Profiles.Domain.Model.Queries.Customer;

namespace SweetManagerWebService.Profiles.Domain.Services.Customer;

public interface ICustomerQueryService
{
    Task<IEnumerable<Model.Aggregates.Customer>> Handle(GetAllCustomersQuery query);
}