using SweetManagerWebService.Shared.Domain.Model.ValueObjects;

namespace SweetManagerWebService.Shared.Domain.Model.Entities;

public class User(string email, string name, string surname, string phoneNumber)
{
    // Unique identifier for the user
    public int Id { get; set; }
    
    // Email address of the user, set during initialization
    public string Email { get; private set; } = email;

    // Full name of the user, encapsulated in the CompleteName value object
    public CompleteName Name { get; private set; } = new CompleteName(name, surname);

    // Phone number of the user
    public string PhoneNumber { get; private set; } = phoneNumber;

    // Default constructor initializing the user with empty values
    public User() : this(string.Empty, string.Empty, string.Empty, string.Empty) { }
}
