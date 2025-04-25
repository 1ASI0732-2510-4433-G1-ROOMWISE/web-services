using SweetManagerWebService.Commerce.Domain.Model.Entities.Contracts;
using SweetManagerWebService.Commerce.Domain.Model.Entities.Payments;
using SweetManagerWebService.Communication.Domain.Model.Aggregates;
using SweetManagerWebService.IAM.Domain.Model.Entities.Credentials;
using SweetManagerWebService.IAM.Domain.Model.Entities.Roles;
using SweetManagerWebService.Profiles.Domain.Model.Entities;

namespace SweetManagerWebService.IAM.Domain.Model.Aggregates
{
    public partial class Owner(int id, string username, string name, string surname, int rolesId,
        int phone, string email, string state)
    {
        public int Id { get; private set; } = id;
        
        public string Username { get; private set; } = username;
        
        public int RolesId { get; private set; } = rolesId;
        
        public string Name { get; private set; } = name.ToUpper();
        public string Surname { get; private set; } = surname.ToUpper();
        public int Phone { get; private set; } = phone;
        public string Email { get; private set; } = email;
        public string State { get; private set; } = state.ToUpper();

        public virtual OwnerCredential? OwnerCredential { get; }
        
        public virtual Role Role { get; } = null!;
        
        public virtual ICollection<ContractOwner> ContractsOwners { get; } = [];
        
        public virtual ICollection<Hotel> Hotels { get; } = [];
        
        public virtual ICollection<Notification> Notifications { get; } = [];
        
        public virtual ICollection<PaymentOwner> PaymentsOwners { get; } = [];
        
    }
}