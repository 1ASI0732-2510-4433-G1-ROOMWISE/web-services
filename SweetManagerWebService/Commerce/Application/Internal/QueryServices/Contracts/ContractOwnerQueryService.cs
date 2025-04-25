using SweetManagerWebService.Commerce.Domain.Model.Entities.Contracts;
using SweetManagerWebService.Commerce.Domain.Model.Queries.Contracts;
using SweetManagerWebService.Commerce.Domain.Repositories.Contracts;
using SweetManagerWebService.Commerce.Domain.Services.Contracts;

namespace SweetManagerWebService.Commerce.Application.Internal.QueryServices.Contracts;

public class ContractOwnerQueryService(IContractOwnerRepository contractOwnerRepository) : IContractOwnerQueryService
{
    public async Task<ContractOwner?> Handle(GetContractOwnerByOwnerIdQuery query)
        => await contractOwnerRepository.FindByOwnerId(query.OwnerId);
    
}