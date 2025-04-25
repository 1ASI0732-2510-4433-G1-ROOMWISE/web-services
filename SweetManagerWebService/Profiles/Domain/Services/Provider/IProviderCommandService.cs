using SweetManagerWebService.Profiles.Domain.Model.Commands.Provider;

namespace SweetManagerWebService.Profiles.Domain.Services.Provider;

public interface IProviderCommandService
{
    Task<bool> Handle (CreateProviderCommand command);
    
    Task<bool> Handle (UpdateProviderCommand command);
}