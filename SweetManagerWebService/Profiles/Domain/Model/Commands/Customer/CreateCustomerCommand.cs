namespace SweetManagerWebService.Profiles.Domain.Model.Commands.Customer;

public record CreateCustomerCommand(
    int Id, 
    string Username,
    string Name, 
    string Surname, 
    string Email, 
    int  Phone, 
    string State
    );