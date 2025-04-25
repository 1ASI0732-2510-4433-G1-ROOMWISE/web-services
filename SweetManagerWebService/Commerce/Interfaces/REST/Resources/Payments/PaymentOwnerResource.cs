namespace SweetManagerWebService.Commerce.Interfaces.REST.Resources.Payments;

public record PaymentOwnerResource(int Id, int OwnerId, string Description, decimal FinalAmount);