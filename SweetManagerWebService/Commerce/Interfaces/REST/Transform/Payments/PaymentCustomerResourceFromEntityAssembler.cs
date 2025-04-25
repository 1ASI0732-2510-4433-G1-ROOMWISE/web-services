using SweetManagerWebService.Commerce.Domain.Model.Entities.Payments;
using SweetManagerWebService.Commerce.Interfaces.REST.Resources.Payments;

namespace SweetManagerWebService.Commerce.Interfaces.REST.Transform.Payments;

public static class PaymentCustomerResourceFromEntityAssembler
{
    public static PaymentCustomerResource ToResourceFromEntity(PaymentCustomer entity)
    {
        return new PaymentCustomerResource(entity.Id, entity.CustomersId, entity.FinalAmount);
    }
}