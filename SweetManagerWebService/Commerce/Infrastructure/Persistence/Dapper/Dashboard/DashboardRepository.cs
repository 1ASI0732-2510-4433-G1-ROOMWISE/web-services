using System.Data;
using Dapper;
using SweetManagerWebService.Commerce.Domain.Repositories.Payments;

namespace SweetManagerWebService.Commerce.Infrastructure.Persistence.Dapper.Dashboard;

public class DashboardRepository(IDbConnection dbConnection) : IDashboardRepository
{
    public async Task<IEnumerable<dynamic>> FindComparativeIncomesAsync(int hotelId)
    {
        string query = $"SELECT " +
                             $"WEEK(pc.created_at) AS week_number,SUM(pc.final_amount) AS total_income,SUM(po.final_amount) AS total_expense,(SUM(pc.final_amount) - SUM(po.final_amount)) AS total_profit" +
                             $" FROM payments_customers pc LEFT JOIN payments_owners po ON WEEK(pc.created_at) = WEEK(po.created_at) AND YEAR(pc.created_at) = YEAR(po.created_at)" +
                             $" JOIN hotels as ho on po.owners_id = ho.owners_id WHERE pc.created_at BETWEEN DATE_SUB(CURDATE(), INTERVAL 4 WEEK) AND CURDATE() AND ho.id = {hotelId}" +
                             $" GROUP BY WEEK(pc.created_at)" +
                             $" ORDER BY week_number";

        var result = await dbConnection.QueryAsync<dynamic>(query, commandType: CommandType.Text);

        return result;
    }
}