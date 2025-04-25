using SweetManagerWebService.Commerce.Domain.Model.Commands.Contracts;
using SweetManagerWebService.Commerce.Interfaces.REST.Resources.Contracts;

namespace SweetManagerWebService.Commerce.Interfaces.REST.Transform.Contracts;

public static class CreateContractOwnerCommandFromResourceAssembler
{
    public static CreateContractOwnerCommand ToCommandFromResource(CreateContractOwnerResource resource)
    {
        return new CreateContractOwnerCommand(resource.SubscriptionId, resource.OwnersId, 
            resource.State);
    }
}