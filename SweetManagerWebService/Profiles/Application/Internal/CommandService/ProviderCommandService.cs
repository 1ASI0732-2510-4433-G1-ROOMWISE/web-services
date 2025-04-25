using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.Profiles.Domain.Model.Commands.Provider;
using SweetManagerWebService.Profiles.Domain.Repositories;
using SweetManagerWebService.Profiles.Domain.Services.Provider;

namespace SweetManagerWebService.Profiles.Application.Internal.CommandService;

public class ProviderCommandService(IProviderRepository providerRepository, IUnitOfWork unitOfWork): IProviderCommandService
{
    public async Task<bool> Handle(CreateProviderCommand command)
    {
        try
        {
            await providerRepository.AddAsync(new(command));
            
            await unitOfWork.CompleteAsync();
            
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<bool> 
        Handle(UpdateProviderCommand command)=> 
        await providerRepository.UpdateProviderStateAsync
        (command.Id,command.Address,command.Email,command.Phone,command.State);
    
}