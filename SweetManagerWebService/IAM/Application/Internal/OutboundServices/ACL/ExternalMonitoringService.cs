using SweetManagerWebService.Monitoring.Interfaces.ACL;

namespace SweetManagerWebService.IAM.Application.Internal.OutboundServices.ACL;

public class ExternalMonitoringService(IMonitoringContextFacade monitoringContextFacade)
{
    public async Task<int> FetchRoomCount(int hotelId)
    {
        return await monitoringContextFacade.GetRoomsCount(hotelId);
    }
}