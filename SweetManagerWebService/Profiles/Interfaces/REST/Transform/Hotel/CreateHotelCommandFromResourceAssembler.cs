using SweetManagerWebService.Profiles.Domain.Model.Commands.Hotel;
using SweetManagerWebService.Profiles.Interfaces.REST.Resources.Hotel;

namespace SweetManagerWebService.Profiles.Interfaces.REST.Transform.Hotel;

public class CreateHotelCommandFromResourceAssembler
{
    public static CreateHotelCommand ToCommandFromResource(CreateHotelResource resource)=>
    new(resource.OwnersId,resource.Name,resource.Description,resource.Address,resource.Phone,resource.Email);
}