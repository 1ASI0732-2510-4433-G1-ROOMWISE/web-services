using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.SupplyManagement.Domain.Model.Entities;

namespace SweetManagerWebService.SupplyManagement.Domain.Repositories;

public interface ISuppliesRequestRepository : IBaseRepository<SuppliesRequest>
{
    public Task<SuppliesRequest?> FindBySupplyId(int supplyId);
    
    public Task<SuppliesRequest?> FindByPaymentOwnerId(int paymentOwnerId);

    public Task<IEnumerable<SuppliesRequest>> FindAllSuppliesRequestsAsync(int HotelId);
}