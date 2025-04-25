using SweetManagerWebService.Commerce.Domain.Model.Aggregates;
using SweetManagerWebService.Commerce.Domain.Model.Queries.Subscriptions;
using SweetManagerWebService.Commerce.Domain.Repositories.Subscriptions;
using SweetManagerWebService.Commerce.Domain.Services.Subscriptions;

namespace SweetManagerWebService.Commerce.Application.Internal.QueryServices.Subscriptions;

public class SubscriptionQueryService(ISubscriptionRepository subscriptionRepository) : ISubscriptionQueryService
{
    public async Task<IEnumerable<Subscription>> Handle(GetAllSubscriptionsQuery query)
        => await subscriptionRepository.ListAsync();

    public async Task<Subscription?> Handle(GetSubscriptionByIdQuery query)
        => await subscriptionRepository.FindByIdAsync(query.Id);
    
}