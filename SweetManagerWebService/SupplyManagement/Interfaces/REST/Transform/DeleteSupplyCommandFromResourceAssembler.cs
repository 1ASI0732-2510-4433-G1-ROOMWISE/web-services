using SweetManagerWebService.SupplyManagement.Domain.Model.Commands;
using SweetManagerWebService.SupplyManagement.Interfaces.REST.Resources;

namespace SweetManagerWebService.SupplyManagement.Interfaces.REST.Transform;

public class DeleteSupplyCommandFromResourceAssembler
{
    public static DeleteSupplyCommand ToCommandFromResource(DeleteSupplyResource resource)
    {
        return new DeleteSupplyCommand(resource.Id);
    }
}