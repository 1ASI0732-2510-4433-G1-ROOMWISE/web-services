using SweetManagerWebService.IAM.Domain.Model.Commands;
using SweetManagerWebService.IAM.Interfaces.REST.Resource.Authentication.User;

namespace SweetManagerWebService.IAM.Interfaces.REST.Transform.Authentication.User;

public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        return new SignInCommand(resource.Email, resource.Password, resource.RolesId);
    }
}