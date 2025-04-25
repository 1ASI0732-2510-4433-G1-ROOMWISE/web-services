namespace SweetManagerWebService.Profiles.Domain.Model.Commands.Provider;

public record UpdateProviderCommand(
    int Id,
    string Address,
    string Email,
    int Phone,
    string State);