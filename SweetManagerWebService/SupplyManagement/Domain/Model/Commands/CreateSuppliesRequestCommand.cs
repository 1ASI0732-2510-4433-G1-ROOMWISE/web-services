namespace SweetManagerWebService.SupplyManagement.Domain.Model.Commands;

public record CreateSuppliesRequestCommand(int PaymentsOwnersId, int SuppliesId, int Count, decimal Amount); 
