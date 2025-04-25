namespace SweetManagerWebService.Profiles.Interfaces.REST.Resources.Provider;

public record UpdateProviderResource(int Id,string Address, string Email, int Phone, string State);