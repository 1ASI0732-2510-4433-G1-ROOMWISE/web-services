using SweetManagerWebService.Commerce.Domain.Model.Entities.Payments;
using SweetManagerWebService.Commerce.Domain.Model.Queries.Payments;

namespace SweetManagerWebService.Commerce.Domain.Services.Payments;

public interface IPaymentOwnerQueryService
{
    Task<IEnumerable<PaymentOwner>> Handle(GetAllPaymentOwnersByOwnerIdQuery query);
    
}