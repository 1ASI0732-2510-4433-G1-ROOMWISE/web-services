using System.Collections;
using SweetManagerWebService.Commerce.Domain.Model.Queries.Dashboard;

namespace SweetManagerWebService.Commerce.Domain.Services.Payments;

public interface IDashboardQueryService
{
    Task<IEnumerable<dynamic>> Handle(GetAdministrationWeeklyExpensesByHotelId query);
    
}