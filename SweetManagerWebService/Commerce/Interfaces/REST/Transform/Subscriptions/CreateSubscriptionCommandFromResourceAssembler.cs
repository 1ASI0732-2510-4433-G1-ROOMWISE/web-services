using SweetManagerWebService.Commerce.Domain.Model.Commands.Subscriptions;
using SweetManagerWebService.Commerce.Interfaces.REST.Resources.Subscriptions;

namespace SweetManagerWebService.Commerce.Interfaces.REST.Transform.Subscriptions;

public static class CreateSubscriptionCommandFromResourceAssembler
{
    public static CreateSubscriptionCommand ToCommandFromResource(CreateSubscriptionResource resource)
    {
        return new CreateSubscriptionCommand(resource.Name, resource.Description, resource.Price, resource.State);
    }
}