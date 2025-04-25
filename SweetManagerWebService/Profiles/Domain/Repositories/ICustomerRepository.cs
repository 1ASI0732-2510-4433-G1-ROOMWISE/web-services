using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.Profiles.Domain.Model.Aggregates;

namespace SweetManagerWebService.Profiles.Domain.Repositories;

public interface ICustomerRepository : IBaseRepository<Customer>
{
    Task<bool> UpdateCustomerStateAsync(int id,string email, int phone, string state);
    
    Task<IEnumerable<Customer>> FindCustomerByHotelIdAsync(int hotelId);
    
}