using SweetManagerWebService.Profiles.Domain.Model.Aggregates;
using SweetManagerWebService.Profiles.Domain.Model.Queries.Provider;
using SweetManagerWebService.Profiles.Domain.Repositories;
using SweetManagerWebService.Profiles.Domain.Services.Provider;

namespace SweetManagerWebService.Profiles.Application.Internal.QueryService;

public class ProviderQueryService(IProviderRepository providerRepository) : IProviderQueryService
{
    public async Task<IEnumerable<Provider>> Handle(GetAllProvidersQuery query) => await providerRepository.FindProviderByHotelIdAsync(query.HotelId);
    
}