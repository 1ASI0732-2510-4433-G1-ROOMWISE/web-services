using SweetManagerWebService.Monitoring.Domain.Model.Commands.Booking;
using SweetManagerWebService.Monitoring.Domain.Model.ValueObjects.Booking;
using SweetManagerWebService.Monitoring.Interfaces.REST.Resources.Booking;

namespace SweetManagerWebService.Monitoring.Interfaces.REST.Transform.Booking
{
    public class UpdateBookingCommandFromResourceAssembler
    {
        public static UpdateBookingStateCommand ToCommandFromResource
            (UpdateBookingStateResource resource) =>
            new(resource.Id, Enum.Parse<EBookingState>
                (resource.BookingState));
    }
}