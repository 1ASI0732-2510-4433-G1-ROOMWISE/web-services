using SweetManagerWebService.Commerce.Domain.Model.Aggregates;
using SweetManagerWebService.Commerce.Domain.Model.Queries.Subscriptions;

namespace SweetManagerWebService.Commerce.Domain.Services.Subscriptions;

public interface ISubscriptionQueryService
{
    Task<IEnumerable<Subscription>> Handle(GetAllSubscriptionsQuery query);

    Task<Subscription?> Handle(GetSubscriptionByIdQuery query);
    
}