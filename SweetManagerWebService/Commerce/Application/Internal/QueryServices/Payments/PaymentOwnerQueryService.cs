using SweetManagerWebService.Commerce.Domain.Model.Entities.Payments;
using SweetManagerWebService.Commerce.Domain.Model.Queries.Payments;
using SweetManagerWebService.Commerce.Domain.Repositories.Payments;
using SweetManagerWebService.Commerce.Domain.Services.Payments;

namespace SweetManagerWebService.Commerce.Application.Internal.QueryServices.Payments;

public class PaymentOwnerQueryService(IPaymentOwnerRepository paymentOwnerRepository) : IPaymentOwnerQueryService
{
    public async Task<IEnumerable<PaymentOwner>> Handle(GetAllPaymentOwnersByOwnerIdQuery query)
        => await paymentOwnerRepository.FindByOwnerId(query.OwnerId);
    
}