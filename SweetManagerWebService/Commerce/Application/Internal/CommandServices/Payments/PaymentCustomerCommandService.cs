using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.Commerce.Domain.Model.Commands.Payments;
using SweetManagerWebService.Commerce.Domain.Model.Entities.Payments;
using SweetManagerWebService.Commerce.Domain.Repositories.Payments;
using SweetManagerWebService.Commerce.Domain.Services.Payments;
using SweetManagerWebService.Profiles.Domain.Model.Queries.Customer;
using SweetManagerWebService.Profiles.Domain.Services.Customer;

namespace SweetManagerWebService.Commerce.Application.Internal.CommandServices.Payments;

public class PaymentCustomerCommandService(IPaymentCustomerRepository paymentCustomerRepository,
    IUnitOfWork unitOfWork, ICustomerQueryService customerQueryService) : IPaymentCustomerCommandService
{
    public async Task<bool> Handle(CreatePaymentCustomerCommand command)
    {
        try
        {
            await paymentCustomerRepository.AddAsync(new PaymentCustomer(command.CustomerId, command.FinalAmount));

            await unitOfWork.CompleteAsync();

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}