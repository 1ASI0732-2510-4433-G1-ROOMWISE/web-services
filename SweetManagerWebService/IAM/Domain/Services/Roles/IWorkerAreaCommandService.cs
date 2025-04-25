
using SweetManagerWebService.IAM.Domain.Model.Commands.Assignments;
using SweetManagerWebService.IAM.Domain.Model.Commands.Role;

namespace SweetManagerWebService.IAM.Domain.Services.Roles;

public interface IWorkerAreaCommandService
{
    Task<bool> Handle(CreateWorkAreaCommand command);

}