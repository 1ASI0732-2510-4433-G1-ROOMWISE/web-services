using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.Profiles.Domain.Model.Aggregates;

namespace SweetManagerWebService.Profiles.Domain.Repositories;

public interface IProviderRepository : IBaseRepository<Provider>
{
    Task<bool> UpdateProviderStateAsync(int id,string address, string email, int phone, string state);
    
    Task<IEnumerable<Provider>> FindProviderByHotelIdAsync(int hotelId);
    
}