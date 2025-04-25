namespace SweetManagerWebService.Commerce.Domain.Model.Commands.Contracts;

public record CreateContractOwnerCommand(int SubscriptionId, int OwnersId, string State);