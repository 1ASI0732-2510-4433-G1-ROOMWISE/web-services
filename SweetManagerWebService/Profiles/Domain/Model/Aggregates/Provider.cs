using SweetManagerWebService.Profiles.Domain.Model.Commands.Provider;
using SweetManagerWebService.SupplyManagement.Domain.Model.Aggregates;

namespace SweetManagerWebService.Profiles.Domain.Model.Aggregates
{
    public partial class Provider
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Phone { get; set; }
        public string State { get; set; } = null!;

        public virtual ICollection<Supply> Supplies { get; } = [];

        public Provider()
        {
            this.Name = string.Empty;
            this.Address = string.Empty;
            this.Email = string.Empty;
            this.Phone = 0;
            this.State = string.Empty;
        }

        public Provider(string name, string address, string email, int phone, string state)
        {
            this.Name = name;
            this.Address = address;
            this.Email = email;
            this.Phone = phone;
            this.State = state;
        }

        public Provider(CreateProviderCommand command)
        {
            this.Id = command.Id;
            this.Name = command.Name.ToUpper();
            this.Address = command.Address;
            this.Email = command.Email;
            this.Phone = command.Phone;
            this.State = command.State.ToUpper();
        }

        public Provider(UpdateProviderCommand command)
        {
            this.Address = command.Address;
            this.Email = command.Email;
            this.Phone = command.Phone;
            this.State = command.State;
        }
    }
}