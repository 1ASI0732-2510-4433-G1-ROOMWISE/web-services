namespace SweetManagerWebService.Commerce.Interfaces.REST.Resources.Contracts;

public record ContractOwnerResource(int Id, int SubscriptionId, int OwnersId, DateTime StartDate, DateTime EndDate, string State);