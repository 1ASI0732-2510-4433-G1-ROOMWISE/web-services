using SweetManagerWebService.Monitoring.Domain.Model.Commands.TypeRoom;

namespace SweetManagerWebService.Monitoring.Domain.Services.TypeRoom
{
    public interface ITypeRoomCommandService
    {
        Task<bool> Handle
            (CreateTypeRoomCommand command);
    }
}