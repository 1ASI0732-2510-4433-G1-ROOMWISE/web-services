using Microsoft.EntityFrameworkCore;
using SweetManagerWebService.Commerce.Domain.Model.Entities.Payments;
using SweetManagerWebService.IAM.Domain.Model.Aggregates;
using SweetManagerWebService.Profiles.Domain.Model.Aggregates;
using SweetManagerWebService.Profiles.Domain.Model.Entities;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Repositories;
using SweetManagerWebService.SupplyManagement.Domain.Model.Aggregates;
using SweetManagerWebService.SupplyManagement.Domain.Model.Entities;
using SweetManagerWebService.SupplyManagement.Domain.Repositories;

namespace SweetManagerWebService.SupplyManagement.Infrastructure.Persistence.Repositories;

public class SuppliesRequestRepository(SweetManagerContext context) : BaseRepository<SuppliesRequest>(context), ISuppliesRequestRepository
{
    public async Task<SuppliesRequest?> FindBySupplyId(int supplyId)
    {
        return await Context.Set<SuppliesRequest>().FirstOrDefaultAsync(f => f.SuppliesId == supplyId);
    }

    public async Task<SuppliesRequest?> FindByPaymentOwnerId(int paymentOwnerId)
    {
        return await Context.Set<SuppliesRequest>().FirstOrDefaultAsync(f => f.PaymentsOwnersId == paymentOwnerId);
    }
    
    public async Task<IEnumerable<SuppliesRequest>> FindAllSuppliesRequestsAsync(int queryHotelId)
    {
        return await Task.Run(() => (
            from sp in Context.Set<SuppliesRequest>().ToList()
            join po in Context.Set<PaymentOwner>().ToList() on sp.PaymentsOwnersId equals po.Id
            join ow in Context.Set<Owner>().ToList() on po.OwnersId equals ow.Id
            join ho in Context.Set<Hotel>().ToList() on ow.Id equals ho.OwnersId
            where ho.Id.Equals(queryHotelId)
            select sp
        ).ToList());
    }
}