using SweetManagerWebService.Communication.Domain.Model.Aggregates;

namespace SweetManagerWebService.Communication.Domain.Model.Entities
{
    public partial class TypeNotification
    {
        public int Id { get; }
        
        public string Name { get; private set; } = null!;

        public TypeNotification()
        {
            
        }

        public TypeNotification(string name)
        {
            Name = name;
        }

        public virtual ICollection<Notification> Notifications { get; } = [];
    }
}