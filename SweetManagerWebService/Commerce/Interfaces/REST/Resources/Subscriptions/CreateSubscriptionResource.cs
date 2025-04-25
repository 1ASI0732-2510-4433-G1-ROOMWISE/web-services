namespace SweetManagerWebService.Commerce.Interfaces.REST.Resources.Subscriptions;

public record CreateSubscriptionResource(string Name, string Description, decimal Price, string State);