using SweetManagerWebService.IAM.Domain.Model.Aggregates;

namespace  SweetManagerWebService.IAM.Domain.Model.Entities.Credentials
{

    public partial class AdminCredential(int adminsId, string code)
    {
        public int AdminsId { get; set; } = adminsId;

        public string Code { get; set; } = code;

        public virtual Admin Admin { get; } = null!;
    }
}