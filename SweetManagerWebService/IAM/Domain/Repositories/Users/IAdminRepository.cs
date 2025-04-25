using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.IAM.Domain.Model.Aggregates;

namespace SweetManagerWebService.IAM.Domain.Repositories.Users;

public interface IAdminRepository : IBaseRepository<Admin>
{
    Task<IEnumerable<Admin>> FindAllByHotelId(int hotelId);

    Task<Admin?> FindById(int id);

    Task<Admin?> FindByEmail(string email);

    Task<int?> FindIdByEmail(string email);

    Task<bool> ExecuteUpdateAdminEmailAsync(string email, int id);

    Task<bool> ExecuteUpdateAdminPhoneAsync(int phone, int id);

    Task<int?> FindHotelIdByAdminId(int id);

}