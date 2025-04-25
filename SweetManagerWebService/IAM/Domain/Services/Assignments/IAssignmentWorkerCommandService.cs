using SweetManagerWebService.IAM.Domain.Model.Commands.Assignments;

namespace SweetManagerWebService.IAM.Domain.Services.Assignments;

public interface IAssignmentWorkerCommandService
{
    Task<bool> Handle(CreateAssignmentWorkerCommand command);
    
}