using SweetManagerWebService.SupplyManagement.Domain.Model.Aggregates;

namespace SweetManagerWebService.Profiles.Domain.Model.Commands.Provider;

public record CreateProviderCommand(
    int Id, 
    string Name,
    string Address,
    string Email,
    int Phone,
    string State);

