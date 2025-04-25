namespace SweetManagerWebService.SupplyManagement.Interfaces.REST.Resources;

public record CreateSuppliesRequestResource(int PaymentsOwnersId, int SuppliesId, int Count, decimal Amount); 
