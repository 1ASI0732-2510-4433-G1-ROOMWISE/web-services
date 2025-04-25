using SweetManagerWebService.Commerce.Domain.Model.Commands.Payments;
using SweetManagerWebService.Commerce.Interfaces.REST.Resources.Payments;

namespace SweetManagerWebService.Commerce.Interfaces.REST.Transform.Payments;

public static class CreatePaymentOwnerCommandFromResourceAssembler
{
    public static CreatePaymentOwnerCommand ToCommandFromResource(CreatePaymentOwnerResource resource)
    {
        return new CreatePaymentOwnerCommand(resource.OwnerId, resource.Description, resource.FinalAmount);
    }
}