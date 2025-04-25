using SweetManagerWebService.Commerce.Domain.Model.Commands.Payments;
using SweetManagerWebService.Commerce.Interfaces.REST.Resources.Payments;

namespace SweetManagerWebService.Commerce.Interfaces.REST.Transform.Payments;

public static class CreatePaymentCustomerCommandFromResourceAssembler
{
    public static CreatePaymentCustomerCommand ToCommandFromResource(CreatePaymentCustomerResource resource)
    {
        return new CreatePaymentCustomerCommand(resource.CustomerId, resource.FinalAmount);
    }
}