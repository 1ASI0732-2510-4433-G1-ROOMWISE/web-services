using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.Monitoring.Domain.Model.Commands.Booking;
using SweetManagerWebService.Monitoring.Domain.Repositories;
using SweetManagerWebService.Monitoring.Domain.Services.Booking;

namespace SweetManagerWebService.Monitoring.Application.Internal.CommandServices
{
    public class BookingCommandService
        (IBookingRepository bookingRepository,
        IUnitOfWork unitOfWork) :
        IBookingCommandService
    {
        public async Task<bool> Handle
            (CreateBookingCommand command)
        {
            try
            {
                await bookingRepository
                    .AddAsync(new(command));

                await unitOfWork.CompleteAsync();

                return true;
            }
            catch (Exception) { return false; }
        }

        public async Task<bool> Handle
            (UpdateBookingStateCommand command) =>
            await bookingRepository.UpdateBookingStateAsync
            (command.Id, command.BookingState);
    }
}