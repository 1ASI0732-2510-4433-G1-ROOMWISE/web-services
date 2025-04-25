using SweetManagerWebService.Monitoring.Domain.Model.Commands.Room;

namespace SweetManagerWebService.Monitoring.Domain.Services.Room
{
    public interface IRoomCommandService
    {
        Task<bool> Handle
            (CreateRoomCommand command);

        Task<bool> Handle
            (UpdateRoomStateCommand command);
    }
}