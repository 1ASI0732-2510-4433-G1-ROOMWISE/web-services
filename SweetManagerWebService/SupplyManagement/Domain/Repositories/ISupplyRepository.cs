using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.SupplyManagement.Domain.Model.Aggregates;

namespace SweetManagerWebService.SupplyManagement.Domain.Repositories;

public interface ISupplyRepository : IBaseRepository<Supply>
{
   public Task<IEnumerable<Supply>> FindByProvidersId(int providersId);

   public Task<IEnumerable<Supply>> FindSuppliesByHotelIdAsync(int hotelId); 

}