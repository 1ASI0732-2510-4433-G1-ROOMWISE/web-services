using SweetManagerWebService.IAM.Domain.Model.Commands.Role;

namespace SweetManagerWebService.IAM.Domain.Services.Roles;

public interface IRoleCommandService
{
    Task<bool> Handle(SeedRolesCommand command);
}