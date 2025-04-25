using System.Collections;
using SweetManagerWebService.Profiles.Domain.Model.Entities;
using SweetManagerWebService.Profiles.Domain.Model.Queries.Hotel;
using SweetManagerWebService.Profiles.Domain.Repositories;
using SweetManagerWebService.Profiles.Domain.Services.Hotel;

namespace SweetManagerWebService.Profiles.Application.Internal.QueryService;

public class HotelQueryService: IHotelQueryService
{
    private readonly IHotelRepository _hotelRepository;

    public HotelQueryService(IHotelRepository hotelRepository)
    {
        _hotelRepository = hotelRepository;
    }

    public async Task<IEnumerable<Hotel>> Handle(GetAllHotelsQuery query)
    {
        return await _hotelRepository.GetAllAsync();
    }

    public async Task<Hotel?> Handle(GetHotelByOwnersIdQuery query)
    {
        return await _hotelRepository.GetByOwnersIdAsync(query.OwnersId);
    }
    
}