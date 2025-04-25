using SweetManagerWebService.IAM.Domain.Model.Aggregates;

namespace SweetManagerWebService.IAM.Domain.Model.Entities.Roles
{
    public partial class Role(string name)
    {
        public int Id { get; }
        
        public string Name { get; private set; } = name;
        
        
        public virtual ICollection<Owner> Owners { get; } = null!;

        public virtual ICollection<Admin> Admins { get; } = [];
        
        public virtual ICollection<Worker> Workers { get; } = [];
    }
}