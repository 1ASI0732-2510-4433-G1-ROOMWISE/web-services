using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace SweetManagerWebService.SupplyManagement.Domain.Model.Entities;

public partial class SuppliesRequest : IEntityWithCreatedUpdatedDate
{

    [NotMapped]
    [Column("Created At")] public DateTimeOffset? CreatedDate { get; set; }
    
    [NotMapped]
    [Column("Updated At")] public DateTimeOffset? UpdatedDate { get; set; }
}