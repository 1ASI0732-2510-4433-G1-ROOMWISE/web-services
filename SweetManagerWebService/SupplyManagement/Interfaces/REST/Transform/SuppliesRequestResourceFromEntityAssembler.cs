using SweetManagerWebService.SupplyManagement.Domain.Model.Entities;
using SweetManagerWebService.SupplyManagement.Interfaces.REST.Resources;

namespace SweetManagerWebService.SupplyManagement.Interfaces.REST.Transform;

public class SuppliesRequestResourceFromEntityAssembler
{
    public static SuppliesRequestResource ToResourceFromEntity(SuppliesRequest resource)
    {
        return new SuppliesRequestResource(resource.Id, resource.PaymentsOwnersId, resource.SuppliesId, resource.Count, resource.Amount);
    }
}