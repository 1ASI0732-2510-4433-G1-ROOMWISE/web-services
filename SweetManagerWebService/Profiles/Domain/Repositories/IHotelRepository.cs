using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.Profiles.Domain.Model.Entities;

namespace SweetManagerWebService.Profiles.Domain.Repositories;

public interface IHotelRepository: IBaseRepository<Hotel>
{
    Task<IEnumerable<Hotel>> GetAllAsync();
    
    Task<Hotel?> GetByOwnersIdAsync(int ownersId);

    Task<bool> UpdateHotelStateAsync(int id,string name, int phone, string email);
}