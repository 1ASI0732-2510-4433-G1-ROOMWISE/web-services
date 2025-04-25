using Microsoft.EntityFrameworkCore;
using SweetManagerWebService.Communication.Domain.Model.Entities;
using SweetManagerWebService.Communication.Domain.Repositories;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerWebService.Communication.Infrastructure.Persistence.EFC.Repositories
{
    public class TypeNotificationRepository(SweetManagerContext context) : BaseRepository<TypeNotification>(context), ITypeNotificationRepository
    {
        public async Task<bool> FindByNameAsync(string name)
            => await Context.Set<TypeNotification>().AnyAsync(t => t.Name.Equals(name));
        
    }
}