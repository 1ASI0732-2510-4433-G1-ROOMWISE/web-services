using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.ResourceManagement.Domain.Model.Entities;

namespace SweetManagerWebService.ResourceManagement.Domain.Repositories
{
    public interface ITypeReportRepository : IBaseRepository<TypeReport>
    {
        Task<bool> FindByNameAsync(string name);
        
    }
}