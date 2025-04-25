using SweetManagerWebService.IAM.Domain.Model.Commands.Authentication.User;
using SweetManagerWebService.IAM.Interfaces.REST.Resource.Authentication.User;

namespace SweetManagerWebService.IAM.Interfaces.REST.Transform.Authentication.User;

public static class UpdateUserCommandFromResourceAssembler
{
    public static UpdateUserCommand ToCommandFromResource(UpdateUserResource resource)
    {
        return new UpdateUserCommand(resource.Id, resource.Attribute, resource.Value);
    }
}