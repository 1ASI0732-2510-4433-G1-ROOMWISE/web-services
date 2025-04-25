namespace SweetManagerWebService.SupplyManagement.Interfaces.REST.Resources;

public record SuppliesRequestResource(int Id, int PaymentsOwnersId, int SuppliesId, int Count, decimal Amount);