namespace SweetManagerWebService.SupplyManagement.Domain.Model.Exceptions;

public class PaymentOwnerNotFoundException : Exception
{
    public PaymentOwnerNotFoundException(string message) : base(message)
    {
    }
}