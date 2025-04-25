using SweetManagerWebService.IAM.Domain.Model.Commands.Assignments;
using SweetManagerWebService.IAM.Interfaces.REST.Resource.Assignments;

namespace SweetManagerWebService.IAM.Interfaces.REST.Transform.Assignments;

public static class CreateAssignmentWorkerCommandFromResourceAssembler
{
    public static CreateAssignmentWorkerCommand ToCommandFromResource(CreateAssignmentWorkerResource resource)
    {
        var startDate = DateTime.Parse(resource.StartDate);

        var finalDate = DateTime.Parse(resource.FinalDate);

        return new(resource.WorkerAreasId, resource.WorkersId, resource.AdminsId, startDate, finalDate,
            resource.State);
    }
}