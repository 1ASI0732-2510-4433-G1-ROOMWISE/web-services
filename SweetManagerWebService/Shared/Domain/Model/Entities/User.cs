using SweetManagerWebService.Shared.Domain.Model.ValueObjects;

namespace SweetManagerWebService.Shared.Domain.Model.Entities;

public class User(string email, string name, string surname, string phoneNumber)
{
    public int Id { get; set; }
    
    public string Email { get; private set; } = email;

    public CompleteName Name { get; private set; } = new CompleteName(name, surname);

    public string PhoneNumber { get; private set; } = phoneNumber;

    public User() : this(string.Empty, string.Empty,  string.Empty, string.Empty){ }
    
}