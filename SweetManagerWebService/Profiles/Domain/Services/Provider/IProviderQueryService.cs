using SweetManagerWebService.Profiles.Domain.Model.Queries.Provider;

namespace SweetManagerWebService.Profiles.Domain.Services.Provider;

public interface IProviderQueryService
{
    Task<IEnumerable<Model.Aggregates.Provider>> Handle(GetAllProvidersQuery query);
}