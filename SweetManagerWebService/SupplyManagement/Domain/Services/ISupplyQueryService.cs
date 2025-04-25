using SweetManagerWebService.SupplyManagement.Domain.Model.Aggregates;
using SweetManagerWebService.SupplyManagement.Domain.Model.Queries;

namespace SweetManagerWebService.SupplyManagement.Domain.Services;

public interface ISupplyQueryService
{
    Task<Supply?> Handle(GetSupplyByIdQuery query);
    
    Task<IEnumerable<Supply>> Handle(GetAllSuppliesQuery query);
    
    
    Task<IEnumerable<Supply>> Handle(GetSupplyByProviderIdQuery query);
    
    
    
    
    
}