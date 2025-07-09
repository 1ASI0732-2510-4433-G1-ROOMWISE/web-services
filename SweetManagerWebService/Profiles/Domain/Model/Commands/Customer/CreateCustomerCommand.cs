namespace SweetManagerWebService.Profiles.Domain.Model.Commands.Customer;

// Class that defines the command to create a new customer in the system
public record CreateCustomerCommand(
    // Unique ID of the customer
    int Id, 
    
    // Username of the customer
    string Username,
    
    // First name of the customer
    string Name, 
    
    // Surname of the customer
    string Surname, 
    
    // Email address of the customer
    string Email, 
    
    // Phone number of the customer
    int Phone, 
    
    // Status indicating whether the customer is active or inactive
    string State
);
