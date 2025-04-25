using SweetManagerWebService.IAM.Domain.Model.Commands.Role;
using SweetManagerWebService.IAM.Interfaces.REST.Resource.Authentication.Role;

namespace SweetManagerWebService.IAM.Interfaces.REST.Transform.Authentication.Role;

public static class CreateWorkAreaCommandFromResourceAssembler
{
    public static CreateWorkAreaCommand ToCommandFromResource(CreateWorkAreaResource resource)
    {
        return new CreateWorkAreaCommand(resource.Name, resource.HotelId);
    }
}