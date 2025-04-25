using Microsoft.AspNetCore.Mvc;
using SweetManagerWebService.Communication.Domain.Model.Queries.TypeNotification;
using SweetManagerWebService.Communication.Domain.Services.TypeNotification;
using SweetManagerWebService.Communication.Interfaces.REST.Transform.TypeNotification;

namespace SweetManagerWebService.Communication.Interfaces.REST;

[Route("api/[controller]")]
[ApiController]
public class TypesNotificationsController(ITypeNotificationQueryService typeNotificationQueryService) :
    ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> AllTypesNotifications()
    {
        var typesNotifications = await typeNotificationQueryService
            .Handle(new GetAllTypesNotificationsQuery());

        var typesNotificationsResource = typesNotifications.Select
        (TypeNotificationResourceFromEntityAssembler
            .ToResourceFromEntity);

        return Ok(typesNotificationsResource);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> TypeNotificationById(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid Id");
        }

        try
        {
            var typeNotification = await typeNotificationQueryService.Handle(new GetTypeNotificationByIdQuery(id));

            if (typeNotification is null)
            {
                throw new Exception("TypeNotification not found");
            }

            var typeNotificationResource = TypeNotificationResourceFromEntityAssembler.ToResourceFromEntity(typeNotification);

            return Ok(typeNotificationResource);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}