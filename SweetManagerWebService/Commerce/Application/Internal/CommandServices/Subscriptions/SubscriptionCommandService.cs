using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.Commerce.Domain.Model.Aggregates;
using SweetManagerWebService.Commerce.Domain.Model.Commands.Subscriptions;
using SweetManagerWebService.Commerce.Domain.Repositories.Subscriptions;
using SweetManagerWebService.Commerce.Domain.Services.Subscriptions;

namespace SweetManagerWebService.Commerce.Application.Internal.CommandServices.Subscriptions;

public class SubscriptionCommandService(ISubscriptionRepository subscriptionRepository,
    IUnitOfWork unitOfWork) : ISubscriptionCommandService
{
    public async Task<bool> Handle(CreateSubscriptionCommand command)
    {
        try
        {
            await subscriptionRepository.AddAsync(new Subscription(command.Name, command.Description, command.Price,
                command.State));

            await unitOfWork.CompleteAsync();
            
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}