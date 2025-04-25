using SweetManagerWebService.Profiles.Interfaces.REST.Resources.Hotel;

namespace SweetManagerWebService.Profiles.Interfaces.REST.Transform.Hotel;

public class HotelResourceFromEntityAssembler
{
    public static HotelResource ToResourceFromEntity(Domain.Model.Entities.Hotel entity) =>
        new(entity.Id, entity.OwnersId, entity.Name, entity.Description, entity.Address,entity.Phone,entity.Email);
}