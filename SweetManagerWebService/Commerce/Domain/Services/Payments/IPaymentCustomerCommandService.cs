using SweetManagerWebService.Commerce.Domain.Model.Commands.Payments;

namespace SweetManagerWebService.Commerce.Domain.Services.Payments;

public interface IPaymentCustomerCommandService
{
    Task<bool> Handle(CreatePaymentCustomerCommand command);
    
}