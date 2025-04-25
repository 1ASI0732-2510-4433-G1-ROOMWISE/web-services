using SweetManagerWebService.Commerce.Domain.Model.Entities.Contracts;

namespace SweetManagerWebService.Commerce.Domain.Model.Aggregates
{
    public partial class Subscription(string name, string description, decimal price, string state)
    {
        public int Id { get; }
        
        public string Name { get; private set; } = name.ToUpper();
        
        public string Description { get; private set; } = description;
        
        public decimal Price { get; private set; } = price;
        
        public string State { get; private set; } = state.ToUpper();

        public virtual ICollection<ContractOwner> ContractsOwners { get; } = [];
    }
}