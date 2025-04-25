namespace SweetManagerWebService.SupplyManagement.Domain.Model.Commands;

public record CreateSupplyCommand(int ProvidersId, string Name, decimal Price, int Stock, string State);