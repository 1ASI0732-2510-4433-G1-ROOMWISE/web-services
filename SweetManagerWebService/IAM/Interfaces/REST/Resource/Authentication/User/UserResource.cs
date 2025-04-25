namespace SweetManagerWebService.IAM.Interfaces.REST.Resource.Authentication.User;

public record UserResource(int Id, int RolesId, string Username, string Name, string Surname, string Email, int Phone, string State);