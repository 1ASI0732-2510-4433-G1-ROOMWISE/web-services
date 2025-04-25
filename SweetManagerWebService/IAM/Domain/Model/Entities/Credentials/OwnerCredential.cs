using SweetManagerWebService.IAM.Domain.Model.Aggregates;

namespace SweetManagerWebService.IAM.Domain.Model.Entities.Credentials
{
    public partial class OwnerCredential(int ownersId, string code)
    {
        public int OwnersId { get; set; } = ownersId;
        
        public string Code { get; set; } = code;

        public virtual Owner Owner { get; } = null!;
    }
}