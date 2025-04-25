namespace SweetManagerWebService.Profiles.Interfaces.REST.Resources.Hotel;

public record HotelResource(int Id, int OwnersId, string Name, string Description, string Address, int Phone, string Email);
