using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.IAM.Domain.Model.Entities.Roles;

namespace SweetManagerWebService.IAM.Domain.Repositories.Roles;

public interface IRoleRepository : IBaseRepository<Role>
{
    Task<IEnumerable<Role>> FindAllAsync();

    Task<Role?> FindByName(string name);

    Task<int?> FindIdByName(string name);
}