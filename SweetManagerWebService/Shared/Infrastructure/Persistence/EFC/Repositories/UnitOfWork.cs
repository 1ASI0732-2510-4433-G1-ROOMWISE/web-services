using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Repositories
{
    public class UnitOfWork(SweetManagerContext context) : IUnitOfWork
    {
        public async Task CompleteAsync() => await context.SaveChangesAsync();
    }
}

