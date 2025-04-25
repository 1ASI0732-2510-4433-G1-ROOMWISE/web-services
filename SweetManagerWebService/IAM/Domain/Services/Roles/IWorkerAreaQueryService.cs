using SweetManagerWebService.IAM.Domain.Model.Entities.Roles;
using SweetManagerWebService.IAM.Domain.Model.Queries;

namespace SweetManagerWebService.IAM.Domain.Services.Roles;

public interface IWorkerAreaQueryService
{
    Task<IEnumerable<WorkerArea>> Handle(GetAllWorkerAreasByHotelIdQuery byHotelIdQuery);
    
    Task<WorkerArea?> Handle(GetWorkerAreaByNameAndHotelIdQuery andHotelIdQuery);

    Task<int?> Handle(GetWorkerAreaIdByRoleNameAndHotelIdQuery andHotelIdQuery);

    Task<string?> Handle(GetWorkerAreaByWorkerId query);
    
}