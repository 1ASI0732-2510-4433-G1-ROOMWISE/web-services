namespace SweetManagerWebService.SupplyManagement.Domain.Model.Exceptions;

public class InvalidSuppliesRequestAmountException : Exception
{
    public InvalidSuppliesRequestAmountException(string message) : base(message)
    {
    }
}