using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.Commerce.Domain.Model.Commands.Payments;
using SweetManagerWebService.Commerce.Domain.Model.Entities.Payments;
using SweetManagerWebService.Commerce.Domain.Repositories.Payments;
using SweetManagerWebService.Commerce.Domain.Services.Payments;

namespace SweetManagerWebService.Commerce.Application.Internal.CommandServices.Payments;

public class PaymentOwnerCommandService(IPaymentOwnerRepository paymentOwnerRepository,
    IUnitOfWork unitOfWork) : IPaymentOwnerCommandService
{
    public async Task<bool> Handle(CreatePaymentOwnerCommand command)
    {
        try
        {
            await paymentOwnerRepository.AddAsync(new PaymentOwner(command.OwnerId, command.Description,
                command.FinalAmount));

            await unitOfWork.CompleteAsync();
            
            return true;
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}