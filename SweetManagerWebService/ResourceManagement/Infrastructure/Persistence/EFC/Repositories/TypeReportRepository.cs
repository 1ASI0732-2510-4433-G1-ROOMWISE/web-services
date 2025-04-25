using Microsoft.EntityFrameworkCore;
using SweetManagerWebService.ResourceManagement.Domain.Model.Entities;
using SweetManagerWebService.ResourceManagement.Domain.Repositories;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerWebService.ResourceManagement.Infrastructure.Persistence.EFC.Repositories
{
    public class TypeReportRepository(SweetManagerContext context) : BaseRepository<TypeReport>(context), ITypeReportRepository
    {
        public async Task<bool> FindByNameAsync(string name)
            => await Context.Set<TypeReport>().AnyAsync(t => t.Name.Equals(name));
    }
}