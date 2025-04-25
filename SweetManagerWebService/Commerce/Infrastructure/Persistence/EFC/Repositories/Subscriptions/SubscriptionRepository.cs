using SweetManagerWebService.Commerce.Domain.Model.Aggregates;
using SweetManagerWebService.Commerce.Domain.Repositories.Subscriptions;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerWebService.Commerce.Infrastructure.Persistence.EFC.Repositories.Subscriptions;

public class SubscriptionRepository(SweetManagerContext context) : BaseRepository<Subscription>(context), ISubscriptionRepository
{
    
}