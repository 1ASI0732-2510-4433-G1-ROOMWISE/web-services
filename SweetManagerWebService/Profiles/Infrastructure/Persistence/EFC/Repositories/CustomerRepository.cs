
 using Microsoft.EntityFrameworkCore;
 using SweetManagerWebService.Commerce.Domain.Model.Entities.Payments;
 using SweetManagerWebService.IAM.Domain.Model.Aggregates;
 using SweetManagerWebService.Monitoring.Domain.Model.Aggregates;
 using SweetManagerWebService.Profiles.Domain.Model.Aggregates;
 using SweetManagerWebService.Profiles.Domain.Model.Entities;
 using SweetManagerWebService.Profiles.Domain.Repositories;
 using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Configuration;
 using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Repositories;

 namespace SweetManagerWebService.Profiles.Infrastructure.Persistence.EFC.Repositories
 {
  public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
  {
   private readonly SweetManagerContext _context;

   public CustomerRepository(SweetManagerContext context) : base(context)
   {
    _context = context;
   }

   public async Task<bool> UpdateCustomerStateAsync(int id,string email, int phone, string state) =>
    await _context.Set<Customer>()
     .Where(c=>c.Id == id ) 
     .ExecuteUpdateAsync(c => c
      .SetProperty(u => u.Email, email)
      .SetProperty(u => u.Phone, phone)
      .SetProperty(u => u.State, state)
     ) > 0;


   public async Task<IEnumerable<Customer>> FindCustomerByHotelIdAsync(int hotelId)
    => await Task.Run(() => (
     from cus in Context.Set<Customer>().ToList()
     join payCus in Context.Set<PaymentCustomer>().ToList() on cus.Id equals payCus.CustomersId
     join book in Context.Set<Booking>().ToList() on payCus.Id equals book.PaymentsCustomersId
     join room in Context.Set<Room>().ToList() on book.RoomsId equals room.Id
     join htl in Context.Set<Hotel>().ToList() on room.HotelsId equals htl.Id
     where htl.Id == hotelId
     select cus
    ).ToList());

  }
 }
