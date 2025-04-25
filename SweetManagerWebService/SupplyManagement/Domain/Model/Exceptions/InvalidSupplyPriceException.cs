namespace SweetManagerWebService.SupplyManagement.Domain.Model.Exceptions;

public class InvalidSupplyPriceException : Exception
{
    public InvalidSupplyPriceException(string message) : base(message)
    {
    }
}