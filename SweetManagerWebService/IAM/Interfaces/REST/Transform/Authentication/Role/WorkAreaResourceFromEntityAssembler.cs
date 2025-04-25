using SweetManagerWebService.IAM.Domain.Model.Entities.Roles;
using SweetManagerWebService.IAM.Interfaces.REST.Resource.Authentication.Role;

namespace SweetManagerWebService.IAM.Interfaces.REST.Transform.Authentication.Role;

public static class WorkAreaResourceFromEntityAssembler
{
    public static WorkAreaResource ToResourceFromEntity(WorkerArea entity)
    {
        return new(entity.Id, entity.Name);
    }
}