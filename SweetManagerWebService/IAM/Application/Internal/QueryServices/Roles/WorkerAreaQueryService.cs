using SweetManagerWebService.IAM.Domain.Model.Entities.Roles;
using SweetManagerWebService.IAM.Domain.Model.Queries;
using SweetManagerWebService.IAM.Domain.Repositories.Roles;
using SweetManagerWebService.IAM.Domain.Services.Roles;

namespace SweetManagerWebService.IAM.Application.Internal.QueryServices.Roles;

public class WorkerAreaQueryService(IWorkerAreaRepository workerAreaRepository) : IWorkerAreaQueryService
{
    public async Task<IEnumerable<WorkerArea>> Handle(GetAllWorkerAreasByHotelIdQuery query)
    {
        return await workerAreaRepository.FindAllAsync(query.HotelId);
    }

    public async Task<WorkerArea?> Handle(GetWorkerAreaByNameAndHotelIdQuery query)
    {
        return await workerAreaRepository.FindByNameAsync(query.Name, query.HotelId);
    }

    public async Task<int?> Handle(GetWorkerAreaIdByRoleNameAndHotelIdQuery query)
    {
        return await workerAreaRepository.FindIdByNameAsync(query.Name, query.HotelId);
    }

    public async Task<string?> Handle(GetWorkerAreaByWorkerId query)
    {
        return await workerAreaRepository.FindByWorkerIdAsync(query.WorkerId);
    }
    
}