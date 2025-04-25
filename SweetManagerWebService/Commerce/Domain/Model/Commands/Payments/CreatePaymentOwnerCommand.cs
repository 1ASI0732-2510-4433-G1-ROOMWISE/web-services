namespace SweetManagerWebService.Commerce.Domain.Model.Commands.Payments;

public record CreatePaymentOwnerCommand(int OwnerId, string Description, decimal FinalAmount);