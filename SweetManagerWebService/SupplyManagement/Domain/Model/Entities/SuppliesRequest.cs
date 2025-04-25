using SweetManagerWebService.Commerce.Domain.Model.Entities.Payments;
using SweetManagerWebService.SupplyManagement.Domain.Model.Aggregates;
using SweetManagerWebService.SupplyManagement.Domain.Model.Commands;

namespace SweetManagerWebService.SupplyManagement.Domain.Model.Entities
{
    public partial class SuppliesRequest
    {
        public int Id { get; }
        public int PaymentsOwnersId { get; set; }
        public int SuppliesId { get; set; }
        public int Count { get; set; }
        public decimal Amount { get; set; }

        public virtual PaymentOwner PaymentOwner { get; } = null!;
        public virtual Supply Supply { get; } = null!;
        
        public SuppliesRequest(int id, int paymentsOwnersId, int suppliesId, int count, decimal amount)
        {
            Id = id;
            PaymentsOwnersId = paymentsOwnersId;
            SuppliesId = suppliesId;
            Count = count;
            Amount = amount;
        }
        
        public SuppliesRequest(CreateSuppliesRequestCommand command)
        {
            PaymentsOwnersId = command.PaymentsOwnersId;
            SuppliesId = command.SuppliesId;
            Count = command.Count;
            Amount = command.Amount;
        }
        
    }
    
    
}