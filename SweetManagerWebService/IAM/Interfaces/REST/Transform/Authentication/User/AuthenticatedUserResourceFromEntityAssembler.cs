using SweetManagerWebService.IAM.Interfaces.REST.Resource.Authentication.User;

namespace SweetManagerWebService.IAM.Interfaces.REST.Transform.Authentication.User;

public static class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(dynamic entity, string token)
    {
        return new AuthenticatedUserResource(entity.Id, entity.Username, token);
    }
}