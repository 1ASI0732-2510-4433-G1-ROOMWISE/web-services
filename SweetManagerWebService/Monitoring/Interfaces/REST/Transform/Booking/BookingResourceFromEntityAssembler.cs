using SweetManagerWebService.Monitoring.Interfaces.REST.Resources.Booking;

namespace SweetManagerWebService.Monitoring.Interfaces.REST.Transform.Booking
{
    public class BookingResourceFromEntityAssembler
    {
        public static BookingResource ToResourceFromEntity
            (Domain.Model.Aggregates.Booking entity) =>
            new(entity.Id, entity.PaymentsCustomersId,
                entity.RoomsId, entity.Description,
                entity.StartDate, entity.FinalDate,
                entity.PriceRoom, entity.NightCount,
                entity.Amount, entity.State);
    }
}