using SweetManagerWebService.IAM.Domain.Model.Aggregates;
using SweetManagerWebService.IAM.Domain.Model.Queries;
using SweetManagerWebService.IAM.Domain.Repositories.Users;
using SweetManagerWebService.IAM.Domain.Services.Users.Admin;

namespace SweetManagerWebService.IAM.Application.Internal.QueryServices.User;

public class AdminQueryService(IAdminRepository adminRepository) : IAdminQueryService
{
    public async Task<IEnumerable<Admin>> Handle(GetAllUsersQuery query)
    {
        return await adminRepository.FindAllByHotelId(query.HotelId);
    }

    public async Task<Admin?> Handle(GetUserByIdQuery query)
    {
        return await adminRepository.FindById(query.Id);
    }

    public async Task<Admin?> Handle(GetUserByEmailQuery query)
    {
        return await adminRepository.FindByEmail(query.Email);
    }

    public async Task<int?> Handle(GetUserIdByEmailQuery query)
    {
        return await adminRepository.FindIdByEmail(query.Email);
    }
}