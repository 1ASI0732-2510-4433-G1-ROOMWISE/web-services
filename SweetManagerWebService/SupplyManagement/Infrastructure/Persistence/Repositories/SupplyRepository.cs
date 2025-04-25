using Microsoft.EntityFrameworkCore;
using SweetManagerWebService.Commerce.Domain.Model.Entities.Payments;
using SweetManagerWebService.IAM.Domain.Model.Aggregates;
using SweetManagerWebService.Profiles.Domain.Model.Entities;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Repositories;
using SweetManagerWebService.SupplyManagement.Domain.Model.Aggregates;
using SweetManagerWebService.SupplyManagement.Domain.Model.Entities;
using SweetManagerWebService.SupplyManagement.Domain.Repositories;

namespace SweetManagerWebService.SupplyManagement.Infrastructure.Persistence.Repositories;

public class SupplyRepository : BaseRepository<Supply>, ISupplyRepository
{
    public SupplyRepository(SweetManagerContext context) : base(context)
    {
    }


    public async Task<IEnumerable<Supply>> FindByProvidersId(int providersId)
    {
        return await Context.Set<Supply>().Where(s => s.ProvidersId == providersId).ToListAsync();
    }
    
    public async Task<IEnumerable<Supply>> FindSuppliesByHotelIdAsync(int hotelId)
    {
        return await Task.Run(() => (
            from supply in Context.Set<Supply>().ToList()
            join suppliesRequest in Context.Set<SuppliesRequest>().ToList()
                on supply.Id equals suppliesRequest.SuppliesId
            join paymentowner in Context.Set<PaymentOwner>().ToList()
                on suppliesRequest.PaymentsOwnersId equals paymentowner.Id
            join owner in Context.Set<Owner>().ToList()
                on paymentowner.OwnersId equals owner.Id
            join hotel in Context.Set<Hotel>().ToList()
                on owner.Id equals hotel.OwnersId
            where hotel.Id == hotelId
            select supply
        ).ToList());
    }
    
}
