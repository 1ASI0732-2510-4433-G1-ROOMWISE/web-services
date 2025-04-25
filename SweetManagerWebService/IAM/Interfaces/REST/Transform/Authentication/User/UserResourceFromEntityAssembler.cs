using SweetManagerWebService.IAM.Interfaces.REST.Resource.Authentication.User;

namespace SweetManagerWebService.IAM.Interfaces.REST.Transform.Authentication.User;

public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(dynamic user)
    {
        return new UserResource(user.Id, user.RolesId, user.Username, user.Name, user.Surname, user.Email, user.Phone,
            user.State);
    }
}