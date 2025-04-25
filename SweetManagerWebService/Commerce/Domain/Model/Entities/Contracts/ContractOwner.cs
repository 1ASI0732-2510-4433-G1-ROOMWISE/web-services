using SweetManagerWebService.Commerce.Domain.Model.Aggregates;
using SweetManagerWebService.IAM.Domain.Model.Aggregates;

namespace SweetManagerWebService.Commerce.Domain.Model.Entities.Contracts
{
    public partial class ContractOwner
    {
        public int Id { get; }
        public int SubscriptionsId { get; private set; }
        public int OwnersId { get; private set; } 
        public DateTime StartDate { get; private set; } 
        public DateTime FinalDate { get; private set; } 
        public string State { get; private set; } 

        public ContractOwner(int subscriptionId, int ownersId, string state)
        {
            SubscriptionsId = subscriptionId;
            OwnersId = ownersId;
            StartDate = DateTime.Now;
            FinalDate = DateTime.Now.AddMonths(1);
            State = state.ToUpper();
        }
        
        public ContractOwner()
        {
            
        }
        
        public virtual Owner Owner { get; } = null!;
        public virtual Subscription Subscription { get; } = null!;
    }
}