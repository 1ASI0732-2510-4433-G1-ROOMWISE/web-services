using SweetManagerWebService.Communication.Domain.Model.Aggregates;
using SweetManagerWebService.IAM.Domain.Model.Entities.Assignments;
using SweetManagerWebService.IAM.Domain.Model.Entities.Credentials;
using SweetManagerWebService.IAM.Domain.Model.Entities.Roles;
using SweetManagerWebService.ResourceManagement.Domain.Model.Aggregates;

namespace SweetManagerWebService.IAM.Domain.Model.Aggregates
{
    public partial class Worker(int id, string username, string name, string surname, int rolesId,
        int phone, string email, string state)
    {
        public int Id { get; private set; } = id;
        public int RolesId { get; private set; } = rolesId;
        public string Username { get; private set; } = username;
        public string Name { get; private set; } = name.ToUpper();
        public string Surname { get; private set; } = surname.ToUpper();
        public int Phone { get; private set; } = phone;
        public string Email { get; private set; } = email;
        public string State { get; private set; } = state.ToUpper();

        public virtual Role Role { get; } = null!;
        
        public virtual WorkerCredential? WorkerCredential { get; }

        public virtual ICollection<AssignmentWorker> AssignmentsWorkers { get; } = [];
        public virtual ICollection<Notification> Notifications { get; } = [];
        public virtual ICollection<Report> Reports { get; } = [];
    }
}