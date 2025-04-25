using SweetManagerWebService.IAM.Domain.Model.Aggregates;
using SweetManagerWebService.IAM.Domain.Model.Queries;
using SweetManagerWebService.IAM.Domain.Repositories.Users;
using SweetManagerWebService.IAM.Domain.Services.Users.Worker;

namespace SweetManagerWebService.IAM.Application.Internal.QueryServices.User;

public class WorkerQueryService(IWorkerRepository workerRepository) : IWorkerQueryService
{
    public async Task<IEnumerable<Worker>> Handle(GetAllUsersQuery query)
    {
        return await workerRepository.FindAllByHotelId(query.HotelId);
    }

    public async Task<Worker?> Handle(GetUserByIdQuery query)
    {
        return await workerRepository.FindById(query.Id);
    }

    public async Task<Worker?> Handle(GetUserByEmailQuery query)
    {
        return await workerRepository.FindByEmail(query.Email);
    }

    public async Task<int?> Handle(GetUserIdByEmailQuery query)
    {
        return await workerRepository.FindIdByEmail(query.Email);
    }
}