using System.ComponentModel.DataAnnotations;

namespace SweetManagerWebService.Communication.Interfaces.REST.Resources.Notification
{
    public record CreateNotificationResource(
        [Required] int TypesNotificationsId,
        int? OwnersId,
        int? AdminsId,
        int? WorkersId,
        [Required] [StringLength(100)] string Title,
        [Required] [StringLength(500)] string Description);
}