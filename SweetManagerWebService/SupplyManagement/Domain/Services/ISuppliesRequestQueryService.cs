using SweetManagerWebService.SupplyManagement.Domain.Model.Entities;
using SweetManagerWebService.SupplyManagement.Domain.Model.Queries;

namespace SweetManagerWebService.SupplyManagement.Domain.Services;

public interface ISuppliesRequestQueryService
{
    Task<SuppliesRequest?> Handle(GetSuppliesRequestByIdQuery query);
    
    Task<IEnumerable<SuppliesRequest>> Handle(GetAllSuppliesRequestQuery query);
    
    Task<SuppliesRequest?> Handle(GetSuppliesRequestByPaymentOwnerIdQuery query);
    
    Task<SuppliesRequest?> Handle(GetSuppliesRequestBySupplyIdQuery query);

}