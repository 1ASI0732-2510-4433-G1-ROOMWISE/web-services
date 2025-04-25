using SweetManagerWebService.Commerce.Domain.Model.Queries.Dashboard;
using SweetManagerWebService.Commerce.Domain.Repositories.Payments;
using SweetManagerWebService.Commerce.Domain.Services.Payments;

namespace SweetManagerWebService.Commerce.Application.Internal.QueryServices.Dashboard;

public class DashboardQueryService(IDashboardRepository dashboardRepository) : IDashboardQueryService
{
    public async Task<IEnumerable<dynamic>> Handle(GetAdministrationWeeklyExpensesByHotelId query)
    {
        return await dashboardRepository.FindComparativeIncomesAsync(query.HotelId);
    }
    
}