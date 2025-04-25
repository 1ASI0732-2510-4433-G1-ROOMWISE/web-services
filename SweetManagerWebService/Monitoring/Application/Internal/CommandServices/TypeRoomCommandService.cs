using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.Monitoring.Domain.Model.Commands.TypeRoom;
using SweetManagerWebService.Monitoring.Domain.Repositories;
using SweetManagerWebService.Monitoring.Domain.Services.TypeRoom;

namespace SweetManagerWebService.Monitoring.Application.Internal.CommandServices
{
    public class TypeRoomCommandService
        (ITypeRoomRepository typeRoomRepository,
        IUnitOfWork unitOfWork) :
        ITypeRoomCommandService
    {
        public async Task<bool> Handle
            (CreateTypeRoomCommand command)
        {
            try
            {
                await typeRoomRepository
                    .AddAsync(new(command));

                await unitOfWork.CompleteAsync();

                return true;
            }
            catch (Exception) { return false; }
        }
    }
}