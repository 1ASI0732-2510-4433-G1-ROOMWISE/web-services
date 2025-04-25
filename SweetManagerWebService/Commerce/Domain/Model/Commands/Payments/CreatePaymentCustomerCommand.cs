namespace SweetManagerWebService.Commerce.Domain.Model.Commands.Payments;

public record CreatePaymentCustomerCommand(int CustomerId, decimal FinalAmount);