using SweetManagerWebService.Monitoring.Domain.Model.Aggregates;
using SweetManagerWebService.Monitoring.Domain.Model.Commands.TypeRoom;

namespace SweetManagerWebService.Monitoring.Domain.Model.Entities
{
    public partial class TypeRoom
    {
        public int Id { get; }
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }

        public virtual ICollection<Room> Rooms { get; } = [];

        public TypeRoom()
        {
            this.Description = string.Empty;
            this.Price = 0;
        }
        public TypeRoom
            (string description, decimal price)
        {
            this.Description = description;
            this.Price = price;
        }
        public TypeRoom
            (CreateTypeRoomCommand command)
        {
            this.Description = command.Description;
            this.Price = command.Price;
        }
    }
}