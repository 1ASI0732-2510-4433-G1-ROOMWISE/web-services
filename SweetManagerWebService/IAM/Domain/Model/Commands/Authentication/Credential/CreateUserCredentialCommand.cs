namespace SweetManagerWebService.IAM.Domain.Model.Commands.Authentication.Credential;

public record CreateUserCredentialCommand(int UserId, string Code);