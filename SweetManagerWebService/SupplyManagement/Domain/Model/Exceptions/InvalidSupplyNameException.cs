namespace SweetManagerWebService.SupplyManagement.Domain.Model.Exceptions;

public class InvalidSupplyNameException : Exception
{
    public InvalidSupplyNameException(string message) : base(message)
    {
    }
}