namespace SweetManagerWebService.Profiles.Interfaces.REST.Resources.Provider;

public record CreateProviderResource(int Id, string Name, string Address, string Email, int Phone, string State);

