using SweetManagerWebService.Profiles.Domain.Model.Commands.Hotel;
using SweetManagerWebService.Profiles.Interfaces.REST.Resources.Hotel;

namespace SweetManagerWebService.Profiles.Interfaces.REST.Transform.Hotel;

public class UpdateHotelCommandFromResourceAssembler
{
    public static UpdateHotelCommand ToCommandFromResource(UpdateHotelResource resource)=>
    new(resource.Id,resource.Name,resource.Phone,resource.Email);
}