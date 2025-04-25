using SweetManagerWebService.Commerce.Domain.Model.Entities.Contracts;
using SweetManagerWebService.Commerce.Domain.Model.Queries.Contracts;

namespace SweetManagerWebService.Commerce.Domain.Services.Contracts;

public interface IContractOwnerQueryService
{
    Task<ContractOwner?> Handle(GetContractOwnerByOwnerIdQuery query);
    
}