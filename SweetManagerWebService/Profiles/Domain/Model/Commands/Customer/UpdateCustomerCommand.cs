namespace SweetManagerWebService.Profiles.Domain.Model.Commands.Customer;

public record UpdateCustomerCommand(int Id,string Email, int Phone, string State);