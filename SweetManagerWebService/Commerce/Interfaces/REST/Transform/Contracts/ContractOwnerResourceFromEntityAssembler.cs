using SweetManagerWebService.Commerce.Domain.Model.Entities.Contracts;
using SweetManagerWebService.Commerce.Interfaces.REST.Resources.Contracts;

namespace SweetManagerWebService.Commerce.Interfaces.REST.Transform.Contracts;

public static class ContractOwnerResourceFromEntityAssembler
{
    public static ContractOwnerResource ToResourceFromEntity(ContractOwner entity)
    {
        return new ContractOwnerResource(entity.Id, entity.SubscriptionsId, entity.OwnersId, entity.StartDate, entity.FinalDate, entity.State);
    }
}