using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.Commerce.Domain.Model.Entities.Payments;

namespace SweetManagerWebService.Commerce.Domain.Repositories.Payments;

public interface IPaymentCustomerRepository : IBaseRepository<PaymentCustomer>
{
    Task<IEnumerable<PaymentCustomer>> FindByCustomerId(int customerId);

    Task<IEnumerable<PaymentCustomer>> FindByHotelId(int hotelId);
    
}