using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.Commerce.Domain.Model.Entities.Contracts;

namespace SweetManagerWebService.Commerce.Domain.Repositories.Contracts;

public interface IContractOwnerRepository : IBaseRepository<ContractOwner>
{
    Task<ContractOwner?> FindByOwnerId(int ownerId);
}