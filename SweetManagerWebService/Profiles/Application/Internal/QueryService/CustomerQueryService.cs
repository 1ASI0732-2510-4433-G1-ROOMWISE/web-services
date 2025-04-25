using SweetManagerWebService.Profiles.Domain.Model.Aggregates;
using SweetManagerWebService.Profiles.Domain.Model.Queries.Customer;
using SweetManagerWebService.Profiles.Domain.Repositories;
using SweetManagerWebService.Profiles.Domain.Services.Customer;

namespace SweetManagerWebService.Profiles.Application.Internal.QueryService;

public class CustomerQueryService (ICustomerRepository customerRepository) : ICustomerQueryService
{
    public async Task<IEnumerable<Customer>> Handle(GetAllCustomersQuery query) => await customerRepository.FindCustomerByHotelIdAsync(query.HotelId);
    
    
}