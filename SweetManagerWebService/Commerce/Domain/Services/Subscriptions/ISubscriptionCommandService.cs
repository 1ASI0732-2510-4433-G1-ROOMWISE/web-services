using SweetManagerWebService.Commerce.Domain.Model.Commands.Subscriptions;

namespace SweetManagerWebService.Commerce.Domain.Services.Subscriptions;

public interface ISubscriptionCommandService
{
    Task<bool> Handle(CreateSubscriptionCommand command);
    
}