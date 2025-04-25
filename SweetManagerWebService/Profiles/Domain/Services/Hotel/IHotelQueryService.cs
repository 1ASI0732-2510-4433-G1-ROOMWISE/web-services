using SweetManagerWebService.Profiles.Domain.Model.Queries.Hotel;

namespace SweetManagerWebService.Profiles.Domain.Services.Hotel;

public interface IHotelQueryService
{
    Task<IEnumerable<Model.Entities.Hotel>> Handle(GetAllHotelsQuery query);
    
    Task<Model.Entities.Hotel?> Handle (GetHotelByOwnersIdQuery query);
}