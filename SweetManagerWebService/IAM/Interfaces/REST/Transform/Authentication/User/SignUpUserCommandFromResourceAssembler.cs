using SweetManagerWebService.IAM.Domain.Model.Commands.Authentication.User;
using SweetManagerWebService.IAM.Interfaces.REST.Resource.Authentication.User;

namespace SweetManagerWebService.IAM.Interfaces.REST.Transform.Authentication.User;

public static class SignUpUserCommandFromResourceAssembler
{
    public static SignUpUserCommand ToCommandFromResource(SignUpUserResource resource)
    {
        return new(resource.Id,  resource.Username, resource.Name, resource.Surname, resource.Email,
            resource.Phone, resource.State, resource.Password);
    }
}