using SweetManagerWebService.Commerce.Domain.Model.Entities.Payments;
using SweetManagerWebService.Commerce.Domain.Repositories.Payments;
using SweetManagerWebService.Monitoring.Domain.Model.Aggregates;
using SweetManagerWebService.Profiles.Domain.Model.Entities;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerWebService.Commerce.Infrastructure.Persistence.EFC.Repositories.Payments;

public class PaymentCustomerRepository(SweetManagerContext context): BaseRepository<PaymentCustomer>(context), IPaymentCustomerRepository
{
    public async Task<IEnumerable<PaymentCustomer>> FindByCustomerId(int customerId)
        => await Task.Run(() => (
            from pc in Context.Set<PaymentCustomer>().ToList()
            where pc.CustomersId.Equals(customerId)
            select pc
        ).ToList());

    public async Task<IEnumerable<PaymentCustomer>> FindByHotelId(int hotelId)
        => await Task.Run(() => (
            from pc in Context.Set<PaymentCustomer>().ToList()
            join bk in Context.Set<Booking>().ToList() on pc.Id equals bk.PaymentsCustomersId
            join ro in Context.Set<Room>().ToList() on bk.RoomsId equals ro.Id
            join ho in Context.Set<Hotel>().ToList() on ro.HotelsId equals ho.Id
            where ho.Id.Equals(hotelId)
            select pc
        ).ToList());
}