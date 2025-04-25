using SweetManagerWebService.IAM.Domain.Model.Aggregates;
using SweetManagerWebService.Monitoring.Domain.Model.Aggregates;
using SweetManagerWebService.Profiles.Domain.Model.Commands.Hotel;

namespace SweetManagerWebService.Profiles.Domain.Model.Entities
{
    public partial class Hotel
    {
        public int Id { get; }
        public int OwnersId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int Phone { get; set; }
        public string Email { get; set; } = null!;

        public virtual Owner Owner { get; } = null!;

        public virtual ICollection<Room> Rooms { get; } = [];


        public Hotel()
        {
            this.OwnersId = 0;
            this.Name = string.Empty;
            this.Description = string.Empty;
            this.Address = string.Empty;
            this.Phone = 0;
            this.Email = string.Empty;
        }

        public Hotel(int ownerId, string name, string description, string address, int phone, string email)
        {
            this.OwnersId = ownerId;
            this.Name = name;
            this.Description = description;
            this.Address = address;
            this.Phone = phone;
            this.Email = email;
        }

        public Hotel(CreateHotelCommand command)
        {
         this.OwnersId = command.OwnersId;
         this.Name = command.Name.ToUpper();
         this.Description = command.Description;
         this.Address = command.Address;
         this.Phone = command.Phone;
         this.Email = command.Email;
        }

        public Hotel(UpdateHotelCommand command)
        {
            this.OwnersId = command.Id;
            this.Name = command.Name.ToUpper();
            this.Phone = command.Phone;
            this.Email = command.Email;
        }
        
    }
}