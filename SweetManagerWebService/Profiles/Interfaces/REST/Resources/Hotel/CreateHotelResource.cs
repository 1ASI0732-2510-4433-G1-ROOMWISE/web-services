namespace SweetManagerWebService.Profiles.Interfaces.REST.Resources.Hotel;

public record CreateHotelResource(int OwnersId, string Name, string Description, string Address,int Phone, string Email);