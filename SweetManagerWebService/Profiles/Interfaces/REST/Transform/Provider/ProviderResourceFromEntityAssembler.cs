using SweetManagerWebService.Profiles.Interfaces.REST.Resources.Provider;

namespace SweetManagerWebService.Profiles.Interfaces.REST.Transform.Provider;

public class ProviderResourceFromEntityAssembler
{
    public static ProviderResource ToResourceFromEntity(Domain.Model.Aggregates.Provider entity) =>
        new(entity.Id, entity.Name, entity.Address, entity.Email, entity.Phone, entity.State);
}