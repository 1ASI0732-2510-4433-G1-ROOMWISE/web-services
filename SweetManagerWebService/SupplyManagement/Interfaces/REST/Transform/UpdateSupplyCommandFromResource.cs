using SweetManagerWebService.SupplyManagement.Domain.Model.Commands;

namespace SweetManagerWebService.SupplyManagement.Interfaces.REST.Transform;

public class UpdateSupplyCommandFromResource
{
    public static UpdateSupplyCommand FromResource(int Id, UpdateSupplyResource resource)
    {
        return new UpdateSupplyCommand(Id,  resource.ProvidersId, resource.Name, resource.Price, resource.Stock, resource.State);
    }
}