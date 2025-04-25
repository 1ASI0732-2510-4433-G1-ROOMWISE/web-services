namespace SweetManagerWebService.SupplyManagement.Interfaces.REST;

public record UpdateSupplyResource(int Id, int ProvidersId, string Name, decimal Price, int Stock, string State);