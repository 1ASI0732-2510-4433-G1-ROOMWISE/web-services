using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.Profiles.Domain.Model.Commands.Hotel;
using SweetManagerWebService.Profiles.Domain.Repositories;
using SweetManagerWebService.Profiles.Domain.Services.Hotel;

namespace SweetManagerWebService.Profiles.Application.Internal.CommandService;

public class HotelCommandService(IHotelRepository hotelRepository,IUnitOfWork unitOfWork):IHotelCommandService
{
    public async Task<bool> Handle(CreateHotelCommand command)
    {
        try
        {
            await hotelRepository.AddAsync(new(command));
            await unitOfWork.CompleteAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    
    public async Task<bool> Handle(UpdateHotelCommand command)=> 
    await hotelRepository.UpdateHotelStateAsync(command.Id,command.Name,command.Phone,command.Email);
}