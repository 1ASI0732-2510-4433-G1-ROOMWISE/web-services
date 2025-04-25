using SweetManagerWebService.Commerce.Domain.Model.Entities.Payments;
using SweetManagerWebService.Commerce.Domain.Model.Queries.Payments;

namespace SweetManagerWebService.Commerce.Domain.Services.Payments;

public interface IPaymentCustomerQueryService
{
    Task<IEnumerable<PaymentCustomer>> Handle(GetAllPaymentCustomersByCustomerIdQuery query);

    Task<IEnumerable<PaymentCustomer>> Handle(GetAllPaymentCustomersByHotelIdQuery query);
    
}