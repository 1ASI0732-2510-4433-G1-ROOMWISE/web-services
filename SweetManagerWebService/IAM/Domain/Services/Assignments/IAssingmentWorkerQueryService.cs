using SweetManagerWebService.IAM.Domain.Model.Commands.Assignments;
using SweetManagerWebService.IAM.Domain.Model.Entities.Assignments;
using SweetManagerWebService.IAM.Domain.Model.Queries;

namespace SweetManagerWebService.IAM.Domain.Services.Assignments;

public interface IAssignmentWorkerQueryService
{
    Task<AssignmentWorker?> Handle(GetAssignmentWorkerByIdQuery query);
    
    Task<IEnumerable<AssignmentWorker>> Handle(GetAssignmentWorkerByWorkerIdQuery query);
    
    Task<IEnumerable<AssignmentWorker>> Handle(GetAssignmentWorkerByAdminIdQuery query);
    
    Task<IEnumerable<AssignmentWorker>> Handle(GetAssignmentWorkerByWorkerAreaIdQuery query);

}