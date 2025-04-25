using SweetManagerWebService.IAM.Domain.Model.Entities.Roles;
using SweetManagerWebService.IAM.Domain.Model.Queries;
using SweetManagerWebService.IAM.Domain.Repositories.Roles;
using SweetManagerWebService.IAM.Domain.Services.Roles;

namespace SweetManagerWebService.IAM.Application.Internal.QueryServices.Roles;

public class RoleQueryService(IRoleRepository roleRepository) : IRoleQueryService
{
    public async Task<IEnumerable<Role>> Handle(GetAllRolesQuery query)
    {
        return await roleRepository.FindAllAsync();
    }

    public async Task<Role?> Handle(GetRoleByNameQuery query)
    {
        return await roleRepository.FindByName(query.Name);
    }

    public async Task<int?> Handle(GetRoleIdByNameQuery query)
    {
        return await roleRepository.FindIdByName(query.Name);
    }
}