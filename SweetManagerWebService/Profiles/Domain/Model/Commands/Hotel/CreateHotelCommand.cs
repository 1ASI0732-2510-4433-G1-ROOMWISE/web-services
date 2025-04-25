namespace SweetManagerWebService.Profiles.Domain.Model.Commands.Hotel;

public record CreateHotelCommand(int OwnersId, string Name, string Description, string Address,int Phone, string Email);


