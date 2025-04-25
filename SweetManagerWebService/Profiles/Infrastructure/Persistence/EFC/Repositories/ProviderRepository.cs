using Microsoft.EntityFrameworkCore;
 using SweetManagerWebService.Commerce.Domain.Model.Entities.Payments;
 using SweetManagerWebService.IAM.Domain.Model.Aggregates;
 using SweetManagerWebService.Profiles.Domain.Model.Aggregates;
 using SweetManagerWebService.Profiles.Domain.Model.Entities;
 using SweetManagerWebService.Profiles.Domain.Repositories;
 using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Configuration;
 using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Repositories;
 using SweetManagerWebService.SupplyManagement.Domain.Model.Aggregates;
 using SweetManagerWebService.SupplyManagement.Domain.Model.Entities;

 namespace SweetManagerWebService.Profiles.Infrastructure.Persistence.EFC.Repositories
 {
  public class ProviderRepository : BaseRepository<Provider>, IProviderRepository
  {
   private readonly SweetManagerContext _context;

   public ProviderRepository(SweetManagerContext context) : base(context)
   {
    _context = context;
   }

   public async Task<bool> UpdateProviderStateAsync(int id,string address, string email, int phone, string state) =>
    await _context.Set<Provider>()
     .Where(c=>c.Id == id )
     .ExecuteUpdateAsync(c => c
      .SetProperty(u => u.Address, address)
      .SetProperty(u => u.Email, email)
      .SetProperty(u => u.Phone, phone)
      .SetProperty(u => u.State, state)
     ) > 0;

   public async Task<IEnumerable<Provider>> FindProviderByHotelIdAsync(int hotelId)
    => await Task.Run(() => (
     from prv in Context.Set<Provider>().ToList()
     join sup in Context.Set<Supply>().ToList() on prv.Id equals sup.ProvidersId
     join supReq in Context.Set<SuppliesRequest>().ToList() on sup.Id equals supReq.SuppliesId
     join payOw in Context.Set<PaymentOwner>().ToList() on supReq.PaymentsOwnersId equals payOw.Id
     join own in Context.Set<Owner>().ToList() on payOw.OwnersId equals own.Id
     join htl in Context.Set<Hotel>().ToList() on own.Id equals htl.OwnersId
     where htl.Id == hotelId
     select prv
    ).ToList());
  }
}
