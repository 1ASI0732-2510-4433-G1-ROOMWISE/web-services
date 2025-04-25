namespace SweetManagerWebService.Commerce.Interfaces.REST.Resources.Payments;

public record CreatePaymentCustomerResource(int CustomerId, decimal FinalAmount);