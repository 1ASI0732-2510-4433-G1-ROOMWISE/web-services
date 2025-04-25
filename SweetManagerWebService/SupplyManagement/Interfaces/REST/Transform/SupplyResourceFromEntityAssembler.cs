using SweetManagerWebService.SupplyManagement.Domain.Model.Aggregates;

namespace SweetManagerWebService.SupplyManagement.Interfaces.REST.Transform;

public class SupplyResourceFromEntityAssembler
{
    public static SupplyResource ToResourceFromEntity(Supply supply)
    {
        return new SupplyResource(supply.Id, supply.ProvidersId, supply.Name, supply.Price, supply.Stock, supply.State);
    }
}