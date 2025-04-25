using SweetManagerWebService.IAM.Domain.Model.Entities.Assignments;
using SweetManagerWebService.IAM.Interfaces.REST.Resource.Assignments;

namespace SweetManagerWebService.IAM.Interfaces.REST.Transform.Assignments;

public static class AssignmentWorkerResourceFromEntityAssembler
{
    public static AssignmentWorkerResource ToResourceFromEntity(AssignmentWorker entity)
    {
        return new AssignmentWorkerResource(entity.Id, entity.WorkersAreasId, entity.WorkersId, entity.AdminsId,
            entity.StartDate, entity.FinalDate, entity.State);
    }
}