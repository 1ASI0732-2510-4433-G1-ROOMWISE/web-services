namespace SweetManagerWebService.SupplyManagement.Interfaces.REST.Resources;

public record CreateSupplyResource(int ProvidersId, string Name, decimal Price, int Stock, string State);