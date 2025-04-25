namespace SweetManagerWebService.Commerce.Interfaces.REST.Resources.Payments;

public record ComparativeIncomeResource(int? WeekNumbers, decimal TotalIncome, decimal TotalExpense, decimal TotalProfit);