namespace SweetManagerWebService.Commerce.Interfaces.REST.Resources.Payments;

public record PaymentCustomerResource(int Id, int CustomerId, decimal FinalAmount);