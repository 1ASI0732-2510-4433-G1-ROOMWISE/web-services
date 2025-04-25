using SweetManagerWebService.Commerce.Domain.Model.Aggregates;
using SweetManagerWebService.Commerce.Interfaces.REST.Resources.Subscriptions;

namespace SweetManagerWebService.Commerce.Interfaces.REST.Transform.Subscriptions;

public static class SubscriptionResourceFromEntityAssembler
{
    public static SubscriptionResource ToResourceFromEntity(Subscription entity)
    {
        return new(entity.Id, entity.Name, entity.Description, entity.Price, entity.State);
    }
}