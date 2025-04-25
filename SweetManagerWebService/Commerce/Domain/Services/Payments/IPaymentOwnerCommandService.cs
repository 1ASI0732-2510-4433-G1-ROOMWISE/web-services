using SweetManagerWebService.Commerce.Domain.Model.Commands.Payments;

namespace SweetManagerWebService.Commerce.Domain.Services.Payments;

public interface IPaymentOwnerCommandService
{
    Task<bool> Handle(CreatePaymentOwnerCommand command);
    
}