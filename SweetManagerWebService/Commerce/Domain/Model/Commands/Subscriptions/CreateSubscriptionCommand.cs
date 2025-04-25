namespace SweetManagerWebService.Commerce.Domain.Model.Commands.Subscriptions;

public record CreateSubscriptionCommand(string Name, string Description, decimal Price, string State);