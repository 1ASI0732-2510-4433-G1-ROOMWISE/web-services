using SweetManagerWebService.IAM.Domain.Model.Aggregates;
using SweetManagerWebService.SupplyManagement.Domain.Model.Entities;

namespace SweetManagerWebService.Commerce.Domain.Model.Entities.Payments
{
    public partial class PaymentOwner
    {
        public int Id { get; }
        
        public int OwnersId { get; private set; } 
        
        public string Description { get; private set; } 
        
        public decimal FinalAmount { get; private set; } 

        public PaymentOwner(int ownerId, string description, decimal finalAmount)
        {
            OwnersId = ownerId;
            Description = description;
            FinalAmount = finalAmount;
        }
        
        public PaymentOwner()
        {
            
        }
        
        public virtual Owner Owner { get; } = null!;

        public virtual ICollection<SuppliesRequest> SuppliesRequests { get; } = [];
    }
}