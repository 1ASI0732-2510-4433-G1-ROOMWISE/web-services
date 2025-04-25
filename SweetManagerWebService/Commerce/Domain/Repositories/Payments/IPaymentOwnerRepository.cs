using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.Commerce.Domain.Model.Entities.Payments;

namespace SweetManagerWebService.Commerce.Domain.Repositories.Payments;

public interface IPaymentOwnerRepository : IBaseRepository<PaymentOwner>
{
    Task<IEnumerable<PaymentOwner>> FindByOwnerId(int ownerId);
    
}