using SweetManagerWebService.Profiles.Domain.Model.Commands.Hotel;

namespace SweetManagerWebService.Profiles.Domain.Services.Hotel;

public interface IHotelCommandService
{
    Task<bool> Handle(CreateHotelCommand command);
    Task<bool> Handle(UpdateHotelCommand command);
}