using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.Monitoring.Domain.Model.Commands.Room;
using SweetManagerWebService.Monitoring.Domain.Repositories;
using SweetManagerWebService.Monitoring.Domain.Services.Room;

namespace SweetManagerWebService.Monitoring.Application.Internal.CommandServices
{
    public class RoomCommandService
        (IRoomRepository roomRepository,
        IUnitOfWork unitOfWork) :
        IRoomCommandService
    {
        public async Task<bool> Handle
            (CreateRoomCommand command)
        {
            try
            {
                await roomRepository
                    .AddAsync(new(command));

                await unitOfWork.CompleteAsync();

                return true;
            }
            catch (Exception) { return false; }
        }

        public async Task<bool> Handle
            (UpdateRoomStateCommand command) =>
            await roomRepository.UpdateRoomStateAsync
            (command.Id, command.RoomState);
    }
}