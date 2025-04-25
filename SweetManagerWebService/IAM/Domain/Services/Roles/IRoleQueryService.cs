using SweetManagerWebService.IAM.Domain.Model.Entities.Roles;
using SweetManagerWebService.IAM.Domain.Model.Queries;

namespace SweetManagerWebService.IAM.Domain.Services.Roles;

public interface IRoleQueryService
{
    Task<IEnumerable<Role>> Handle(GetAllRolesQuery query);

    Task<Role?> Handle(GetRoleByNameQuery query);

    Task<int?> Handle(GetRoleIdByNameQuery query);
    
}