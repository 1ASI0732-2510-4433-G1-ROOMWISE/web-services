using SweetManagerWebService.Commerce.Domain.Model.Entities.Payments;
using SweetManagerWebService.Commerce.Interfaces.REST.Resources.Payments;

namespace SweetManagerWebService.Commerce.Interfaces.REST.Transform.Payments;

public static class PaymentOwnerResourceFromEntityAssembler
{
    public static PaymentOwnerResource ToResourceFromEntity(PaymentOwner entity)
    {
        return new PaymentOwnerResource(entity.Id, entity.OwnersId, entity.Description, entity.FinalAmount);
    }
}