using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace SweetManagerWebService.SupplyManagement.Domain.Model.Aggregates;

public partial class Supply : IEntityWithCreatedUpdatedDate
{
    [Column("Created At")] public DateTimeOffset? CreatedDate { get; set; }
    
    [Column("Updated At")] public DateTimeOffset? UpdatedDate { get; set; }
} 