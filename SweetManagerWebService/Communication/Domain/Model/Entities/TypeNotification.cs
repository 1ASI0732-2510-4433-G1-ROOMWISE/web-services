using SweetManagerWebService.Communication.Domain.Model.Aggregates;

namespace SweetManagerWebService.Communication.Domain.Model.Entities
{
     // Represents a notification type/category
    public partial class TypeNotification
    {
        // Unique ID for the notification type
        public int Id { get; }

        // Name of the notification type (private setter)
        public string Name { get; private set; } = null!;

        public TypeNotification()
        {
            
        }

        public TypeNotification(string name)
        {
            Name = name;
        }
        
        // Collection of notifications that use this type
        public virtual ICollection<Notification> Notifications { get; } = [];
    }
}
