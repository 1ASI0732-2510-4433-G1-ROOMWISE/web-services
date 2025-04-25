using SweetManagerWebService.Commerce.Domain.Model.Entities.Contracts;
using SweetManagerWebService.Commerce.Domain.Repositories.Contracts;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerWebService.Commerce.Infrastructure.Persistence.EFC.Repositories.Contracts;

public class ContractOwnerRepository(SweetManagerContext context) : BaseRepository<ContractOwner>(context), IContractOwnerRepository
{
    public async Task<ContractOwner?> FindByOwnerId(int ownerId)
        => await Task.Run(() => (
            from co in Context.Set<ContractOwner>().ToList()
            where co.OwnersId.Equals(ownerId)
            select co
        ).FirstOrDefault());
}