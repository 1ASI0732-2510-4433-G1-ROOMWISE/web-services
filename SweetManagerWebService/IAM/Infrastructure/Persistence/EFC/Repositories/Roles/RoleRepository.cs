using SweetManagerWebService.IAM.Domain.Model.Entities.Roles;
using SweetManagerWebService.IAM.Domain.Repositories.Roles;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerWebService.IAM.Infrastructure.Persistence.EFC.Repositories.Roles;

public class RoleRepository(SweetManagerContext context) : BaseRepository<Role>(context) , IRoleRepository
{
    public async Task<IEnumerable<Role>> FindAllAsync()
        => await Task.Run(() => (
            from rl in Context.Set<Role>().ToList()
            select rl
        ).ToList());

    public async Task<Role?> FindByName(string name)
        => await Task.Run(() => (
            from rl in Context.Set<Role>().ToList()
            where rl.Name.Equals(name)
            select rl
        ).FirstOrDefault());
    
    public async Task<int?> FindIdByName(string name)
        => await Task.Run(() => (
            from rl in Context.Set<Role>().ToList()
            where rl.Name.Equals(name)
            select rl.Id
        ).FirstOrDefault());

}