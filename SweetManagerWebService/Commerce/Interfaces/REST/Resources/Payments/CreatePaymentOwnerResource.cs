namespace SweetManagerWebService.Commerce.Interfaces.REST.Resources.Payments;

public record CreatePaymentOwnerResource(int OwnerId, string Description, decimal FinalAmount);