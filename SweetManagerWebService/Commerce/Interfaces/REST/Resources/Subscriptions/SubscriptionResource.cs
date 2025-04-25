namespace SweetManagerWebService.Commerce.Interfaces.REST.Resources.Subscriptions;

public record SubscriptionResource(int Id, string Name, string Description, decimal Price, string State);