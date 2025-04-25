using System.Diagnostics.CodeAnalysis;
using SweetManagerWebService.Commerce.Interfaces.REST.Resources.Payments;

namespace SweetManagerWebService.Commerce.Interfaces.REST.Transform.Payments;

public static class ComparativeIncomeResourceFromEntityAssembler
{
    // {DapperRow, week_number = '41', total_income = '350.00', total_expense = '30.40', total_profit = '319.60'}
    public static ComparativeIncomeResource ToResourceFromEntity(dynamic entity)
    {
        return new ComparativeIncomeResource(entity.week_number, entity.total_income, entity.total_expense,
            entity.total_profit);
    }
}