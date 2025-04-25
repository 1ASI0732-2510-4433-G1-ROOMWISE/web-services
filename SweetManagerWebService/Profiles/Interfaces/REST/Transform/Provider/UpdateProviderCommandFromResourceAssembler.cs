using SweetManagerWebService.Profiles.Domain.Model.Commands.Provider;
using SweetManagerWebService.Profiles.Interfaces.REST.Resources.Provider;

namespace SweetManagerWebService.Profiles.Interfaces.REST.Transform.Provider;

public class UpdateProviderCommandFromResourceAssembler
{
    public static UpdateProviderCommand ToCommandFromResource(UpdateProviderResource resource) => 
    new(resource.Id,resource.Address,resource.Email,resource.Phone,resource.State);
}
