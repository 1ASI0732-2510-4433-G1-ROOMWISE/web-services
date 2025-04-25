
namespace SweetManagerWebService.Commerce.Domain.Repositories.Payments;

public interface IDashboardRepository
{
    Task<IEnumerable<dynamic>> FindComparativeIncomesAsync(int hotelId);
    
}