using SweetManagerWebService.Monitoring.Domain.Model.Commands.Room;
using SweetManagerWebService.Monitoring.Domain.Model.Entities;
using SweetManagerWebService.Monitoring.Domain.Model.ValueObjects.Room;
using SweetManagerWebService.Profiles.Domain.Model.Entities;

namespace SweetManagerWebService.Monitoring.Domain.Model.Aggregates
{
    public partial class Room
    {
        public int Id { get; }
        public int TypesRoomsId { get; set; }
        public int HotelsId { get; set; }
        public string State { get; set; } = null!;

        public virtual Hotel Hotel { get; } = null!;
        public virtual TypeRoom TypeRoom { get; } = null!;

        public virtual ICollection<Booking> Bookings { get; } = [];

        public Room()
        {
            this.TypesRoomsId = 0;
            this.HotelsId = 0;
            this.State = string.Empty;
        }
        public Room(int typeRoomId, int hotelId, ERoomState roomState)
        {
            this.TypesRoomsId = typeRoomId;
            this.HotelsId = hotelId;
            this.State = roomState.ToString();
        }
        public Room
            (CreateRoomCommand command)
        {
            this.TypesRoomsId = command.TypeRoomId;
            this.HotelsId = command.HotelId;
            this.State = command.RoomState.ToString();
        }
        public Room
            (UpdateRoomStateCommand command)
        {
            this.Id = command.Id;
            this.State = command.RoomState.ToString();
        }
    }
}