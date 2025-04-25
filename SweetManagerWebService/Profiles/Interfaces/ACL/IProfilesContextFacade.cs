using SweetManagerWebService.Profiles.Domain.Model.Aggregates;

namespace SweetManagerWebService.Profiles.Interfaces.ACL;

public interface IProfilesContextFacade
{
    Task<IEnumerable<Customer>> FetchCustomersByHotelId(int hotelId);
    
    
}