using SweetManagerWebService.Profiles.Domain.Model.Aggregates;
using SweetManagerWebService.Profiles.Domain.Model.Queries.Customer;
using SweetManagerWebService.Profiles.Domain.Services.Customer;

namespace SweetManagerWebService.Profiles.Interfaces.ACL.Services;

public class ProfilesContextFacade(ICustomerQueryService customerQueryService) : IProfilesContextFacade
{
    public async Task<IEnumerable<Customer>> FetchCustomersByHotelId(int hotelId)
    => await customerQueryService.Handle(new GetAllCustomersQuery(hotelId));
    
}