using SweetManagerWebService.IAM.Domain.Model.Aggregates;

namespace SweetManagerWebService.IAM.Domain.Model.Entities.Credentials
{
    public partial class WorkerCredential(int workersId, string code)
    {
        public int WorkersId { get; private set; } = workersId;
        public string Code { get; private set; } = code;
        
        public virtual Worker Worker { get; private set; } = null!;
    }
}