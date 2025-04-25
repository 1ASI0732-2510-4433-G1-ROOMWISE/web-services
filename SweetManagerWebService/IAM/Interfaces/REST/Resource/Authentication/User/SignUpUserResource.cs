namespace SweetManagerWebService.IAM.Interfaces.REST.Resource.Authentication.User;

public record SignUpUserResource(int Id, string Username, 
    string Name, string Surname, string Email, int Phone, string State, string Password);
