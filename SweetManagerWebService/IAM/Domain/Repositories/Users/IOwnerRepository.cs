using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.IAM.Domain.Model.Aggregates;

namespace SweetManagerWebService.IAM.Domain.Repositories.Users;

public interface IOwnerRepository : IBaseRepository<Owner>
{
    Task<Owner?> FindByHotelId(int hotelId);

    Task<Owner?> FindById(int id);

    Task<Owner?> FindByEmail(string email);

    Task<int?> FindIdByEmail(string email);
    
    Task<bool> ExecuteUpdateOwnerEmailAsync(string email, int id);

    Task<bool> ExecuteUpdateOwnerPhoneAsync(int phone, int id);

    Task<int?> FindHotelIdByOwnerId(int id);
    
}