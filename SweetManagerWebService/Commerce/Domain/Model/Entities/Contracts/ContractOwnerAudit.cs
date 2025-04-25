using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace SweetManagerWebService.Commerce.Domain.Model.Entities.Contracts;

public partial class ContractOwner : IEntityWithCreatedUpdatedDate
{
    [Column("created_at")] public DateTimeOffset? CreatedDate { get; set; }
    
    [Column("updated_at")] public DateTimeOffset? UpdatedDate { get; set; }
}