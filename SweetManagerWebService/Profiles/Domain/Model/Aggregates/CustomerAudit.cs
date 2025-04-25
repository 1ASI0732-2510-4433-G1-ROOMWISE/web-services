using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace SweetManagerWebService.Profiles.Domain.Model.Aggregates;

public partial class Customer: IEntityWithCreatedUpdatedDate
{
    
    
    [Column("CreatedAt")] public DateTimeOffset? CreatedDate { get; set; }
    
    
    [Column("UpdateAt")] public DateTimeOffset? UpdatedDate { get; set; }
}