namespace SweetManagerWebService.SupplyManagement.Domain.Model.Exceptions;

public class InvalidSupplyStockException : Exception
{
    public InvalidSupplyStockException(string message) : base(message)
    {
    }
}