using SweetManagerWebService.SupplyManagement.Domain.Model.Commands;
using SweetManagerWebService.SupplyManagement.Domain.Model.Entities;
using SweetManagerWebService.SupplyManagement.Interfaces.REST.Resources;

namespace SweetManagerWebService.SupplyManagement.Interfaces.REST.Transform;

public class CreateSuppliesRequestCommandFromResourceAssembler
{
    public static CreateSuppliesRequestCommand ToCommandFromResource(CreateSuppliesRequestResource resource)
    {
        return new CreateSuppliesRequestCommand(
            resource.PaymentsOwnersId,
            resource.SuppliesId,
            resource.Count,
            resource.Amount
        );
    }
}