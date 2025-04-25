using SweetManagerWebService.Commerce.Domain.Model.Entities.Payments;
using SweetManagerWebService.Commerce.Domain.Model.Queries.Payments;
using SweetManagerWebService.Commerce.Domain.Repositories.Payments;
using SweetManagerWebService.Commerce.Domain.Services.Payments;

namespace SweetManagerWebService.Commerce.Application.Internal.QueryServices.Payments;

public class PaymentCustomerQueryService(IPaymentCustomerRepository paymentCustomerRepository) : IPaymentCustomerQueryService
{
    public async Task<IEnumerable<PaymentCustomer>> Handle(GetAllPaymentCustomersByCustomerIdQuery query)
        => await paymentCustomerRepository.FindByCustomerId(query.CustomerId);

    public async Task<IEnumerable<PaymentCustomer>> Handle(GetAllPaymentCustomersByHotelIdQuery query)
        => await paymentCustomerRepository.FindByHotelId(query.Hoteld);
}