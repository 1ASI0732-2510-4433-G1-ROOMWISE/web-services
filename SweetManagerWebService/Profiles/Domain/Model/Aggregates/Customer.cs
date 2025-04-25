using SweetManagerWebService.Commerce.Domain.Model.Entities.Payments;
using SweetManagerWebService.Profiles.Domain.Model.Commands.Customer;

namespace SweetManagerWebService.Profiles.Domain.Model.Aggregates
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Phone { get; set; }
        public string State { get; set; } = null!;

        public virtual ICollection<PaymentCustomer> PaymentsCustomers { get; } = [];

        public Customer()
        {
            this.Username = string.Empty; 
            this.Name = string.Empty;
            this.Surname = string.Empty;
            this.Email = string.Empty; //
            this.Phone = 0;
            this.State = string.Empty;//
        }

        public Customer(string username, string name, string surname, string email, int phone, string state)
        {
            this.Username = username;
            this.Name = name;
            this.Surname = surname;
            this.Email = email;
            this.Phone = phone; 
            this.State = state;
        }

        public Customer(CreateCustomerCommand command)
        {
            this.Id = command.Id;
            this.Username = command.Username;
            this.Name = command.Name.ToUpper();
            this.Surname = command.Surname.ToUpper();
            this.Email = command.Email;
            this.Phone = command.Phone;
            this.State = command.State.ToUpper();
        }

        public Customer(UpdateCustomerCommand command)
        {
            this.Email = command.Email;
            this.Phone = command.Phone;
            this.State = command.State.ToUpper();
        }
    }
}