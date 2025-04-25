using SweetManagerWebService.Profiles.Domain.Model.Commands.Provider;
using SweetManagerWebService.Profiles.Interfaces.REST.Resources.Provider;

namespace SweetManagerWebService.Profiles.Interfaces.REST.Transform.Provider;

public class CreateProviderCommandFromResourceAssembler
{
    public static CreateProviderCommand ToCommandFromResource(CreateProviderResource resource) =>
    new(resource.Id, resource.Name,resource.Address,resource.Email,resource.Phone,resource.State);
}