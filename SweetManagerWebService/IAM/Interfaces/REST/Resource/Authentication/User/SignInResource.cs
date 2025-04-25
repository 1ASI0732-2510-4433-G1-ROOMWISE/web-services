namespace SweetManagerWebService.IAM.Interfaces.REST.Resource.Authentication.User;

public record SignInResource(string Email, string Password, int RolesId);