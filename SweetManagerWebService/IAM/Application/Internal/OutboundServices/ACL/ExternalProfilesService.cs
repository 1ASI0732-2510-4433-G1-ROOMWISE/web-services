using SweetManagerWebService.Profiles.Domain.Model.Aggregates;
using SweetManagerWebService.Profiles.Interfaces.ACL;

namespace SweetManagerWebService.IAM.Application.Internal.OutboundServices.ACL;

public class ExternalProfilesService(IProfilesContextFacade profilesContextFacade)
{
    public async Task<IEnumerable<Customer>> FetchAllCustomersByHotelId(int hotelId)
        => await profilesContextFacade.FetchCustomersByHotelId(hotelId);
}