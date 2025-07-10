using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using SweetManagerWebService.Communication.Domain.Model.Queries.Notificacion;
using SweetManagerWebService.Communication.Domain.Services.Notification;
using SweetManagerWebService.Communication.Interfaces.REST.Resources.Notification;
using SweetManagerWebService.Communication.Interfaces.REST.Transform.Notification;

namespace SweetManagerWebService.Communication.Interfaces.REST
{
    [Route("api/[controller]")] // Base route: /api/notifications
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)] // Returns JSON responses
    public class NotificationsController(
        INotificationCommandService notificationCommandService,  // For write operations
        INotificationQueryService notificationQueryService) : ControllerBase // For read operations
    {
        // POST: api/notifications
        // Creates a new notification
        [HttpPost]
        public async Task<IActionResult> CreateNotification([FromBody] CreateNotificationResource resource)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await notificationCommandService.Handle(CreateNotificationCommandFromResourceAssembler.ToCommandFromResource(resource));

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/notifications/get-all-notifications?hotelId={id}
        // Gets all notifications for a specific hotel
        [HttpGet("get-all-notifications")]
        public async Task<IActionResult> AllNotifications([FromQuery] int hotelId)
        {
            var notifications = await notificationQueryService.Handle(new GetAllNotificationsQuery(hotelId));

            var notificationsResource = notifications.Select(NotificationResourceFromEntityAssembler.ToResourceFromEntity);

            return Ok(notificationsResource);
        }

        // GET: api/notifications/get-notification-by-id?id={id}
        // Gets a single notification by ID
        [HttpGet("get-notification-by-id")]
        public async Task<IActionResult> NotificationById([FromQuery] int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Id");
            }

            try
            {
                var notification = await notificationQueryService.Handle(new GetNotificationByIdQuery(id));

                if (notification is null)
                {
                    throw new Exception("Notification not found");
                }

                var notificationResource = NotificationResourceFromEntityAssembler.ToResourceFromEntity(notification);

                return Ok(notificationResource);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // GET: api/notifications/get-all-notifications-by-workerId?workerId={id}
        // Gets all notifications for a specific worker
        [HttpGet("get-all-notifications-by-workerId")]
        public async Task<IActionResult> GetAllNotificationsByWorkerId([FromQuery] int workerId)
        {
            try
            {
                var notifications =
                    await notificationQueryService.Handle(new GetAllNotificationsByWorkerIdQuery(workerId));

                var notificationResources =
                    notifications.Select(NotificationResourceFromEntityAssembler.ToResourceFromEntity);

                return Ok(notificationResources);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

         // GET: api/notifications/get-all-notifications-admins?hotelId={id}
        // Gets all admin-specific notifications for a hotel
        [HttpGet("get-all-notifications-admins")]
        public async Task<IActionResult> GetAllNotificationsForAdmins([FromQuery] int hotelId)
        {
            try
            {
                var notifications =
                    await notificationQueryService.Handle(
                        new GetAllNotificationsByHotelIdAndExistOwnersIdQuery(hotelId));

                var notificationResources =
                    notifications.Select(NotificationResourceFromEntityAssembler.ToResourceFromEntity);

                return Ok(notificationResources);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
