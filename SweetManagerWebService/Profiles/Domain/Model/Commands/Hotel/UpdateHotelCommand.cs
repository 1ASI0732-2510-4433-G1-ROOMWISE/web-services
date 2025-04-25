namespace SweetManagerWebService.Profiles.Domain.Model.Commands.Hotel;

public record UpdateHotelCommand(int Id,string Name, int Phone, string Email);