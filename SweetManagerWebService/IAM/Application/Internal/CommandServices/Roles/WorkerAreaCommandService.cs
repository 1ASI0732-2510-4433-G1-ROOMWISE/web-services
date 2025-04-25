using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.IAM.Domain.Model.Commands.Role;
using SweetManagerWebService.IAM.Domain.Model.Entities.Roles;
using SweetManagerWebService.IAM.Domain.Model.Exceptions;
using SweetManagerWebService.IAM.Domain.Model.Queries;
using SweetManagerWebService.IAM.Domain.Repositories.Roles;
using SweetManagerWebService.IAM.Domain.Services.Roles;

namespace SweetManagerWebService.IAM.Application.Internal.CommandServices.Roles;

public class WorkerAreaCommandService(IUnitOfWork unitOfWork, IWorkerAreaRepository workerAreaRepository, IWorkerAreaQueryService workerAreaQueryService) : IWorkerAreaCommandService
{
    public async Task<bool> Handle(CreateWorkAreaCommand command)
    {
        try
        {
            if (await workerAreaQueryService.Handle(new GetWorkerAreaByNameAndHotelIdQuery(command.Name, command.HotelId)) is
                not null)
                throw new WorkAreaWithTheGivenNameAlreadyExistException();
            
            await workerAreaRepository.AddAsync(new WorkerArea(command.Name));

            await unitOfWork.CompleteAsync();
            
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}