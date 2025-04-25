using SweetManagerWebService.Commerce.Domain.Model.Entities.Payments;
using SweetManagerWebService.Commerce.Domain.Repositories.Payments;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerWebService.Commerce.Infrastructure.Persistence.EFC.Repositories.Payments;

public class PaymentOwnerRepository(SweetManagerContext context) : BaseRepository<PaymentOwner>(context), IPaymentOwnerRepository
{
    public async Task<IEnumerable<PaymentOwner>> FindByOwnerId(int ownerId)
        => await Task.Run(() => (
            from po in Context.Set<PaymentOwner>().ToList()
            where po.OwnersId.Equals(ownerId)
            select po
        ).ToList());
}