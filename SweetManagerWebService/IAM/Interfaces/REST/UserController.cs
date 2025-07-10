using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SweetManagerWebService.IAM.Application.Internal.OutboundServices.ACL;
using SweetManagerWebService.IAM.Domain.Model.Queries;
using SweetManagerWebService.IAM.Domain.Services.Users.Admin;
using SweetManagerWebService.IAM.Domain.Services.Users.Owner;
using SweetManagerWebService.IAM.Domain.Services.Users.Worker;
using SweetManagerWebService.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using SweetManagerWebService.IAM.Interfaces.REST.Resource.Authentication.User;
using SweetManagerWebService.IAM.Interfaces.REST.Transform.Authentication.User;

namespace SweetManagerWebService.IAM.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")] // Base route: /api/v1/user
[Produces(MediaTypeNames.Application.Json)] // Returns JSON responses
public class UserController(IWorkerCommandService workerCommandService,
    IAdminCommandService adminCommandService, 
    IOwnerCommandService ownerCommandService,
    IAdminQueryService adminQueryService,
    IWorkerQueryService workerQueryService,
    IOwnerQueryService ownerQueryService,
    ExternalMonitoringService externalMonitoringService,
    ExternalProfilesService externalProfilesService) : ControllerBase
{
    // GET: api/v1/user/get-owner-id?id={id}
    // Retrieves owner details by ID
    [HttpGet("get-owner-id")]
    public async Task<IActionResult> GetOwnerById([FromQuery] int id)
    {
        try
        {
            var owner = await ownerQueryService.Handle(new GetUserByIdQuery(id));

            var ownerResource = UserResourceFromEntityAssembler.ToResourceFromEntity(owner!);

            return Ok(ownerResource);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // GET: api/v1/user/get-all-admins?hotelId={id}
    // Retrieves all admins for a specific hotel
    [HttpGet("get-all-admins")]
    public async Task<IActionResult> GetAdmins([FromQuery]int hotelId)
    {
        try
        {
            var admins = await adminQueryService.Handle(new GetAllUsersQuery(hotelId));

            var adminResources = admins.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
            
            return Ok(adminResources);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    // GET: api/v1/user/get-all-workers?hotelId={id}
    // Retrieves all workers for a specific hotel
    [HttpGet("get-all-workers")]
    public async Task<IActionResult> getAllWorkers([FromQuery]int hotelId)
    {
        try
        {
            var workers = await workerQueryService.Handle(new GetAllUsersQuery(hotelId));

            var workerResources = workers.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
            
            return Ok(workerResources);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // GET: api/v1/user/get-admin-count?hotelId={id}
    // Returns count of admins for a hotel (requires authorization)
    [HttpGet("get-admin-count")]
    [Authorize]
    public async Task<IActionResult> GetAdminCount([FromQuery]int hotelId)
    {
        try
        {
            var admins = await adminQueryService.Handle(new GetAllUsersQuery(hotelId));

            var result = admins.Count();

            return Ok(new { count = result});
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // GET: api/v1/user/get-worker-count?hotelId={id}
    // Returns count of workers for a hotel (requires authorization)
    [HttpGet("get-worker-count")]
    [Authorize]
    public async Task<IActionResult> GetWorkerCount([FromQuery] int hotelId)
    {
        try
        {
            var workers = await workerQueryService.Handle(new GetAllUsersQuery(hotelId));

            var result = workers.Count();

            return Ok(new { count = result });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // GET: api/v1/user/get-room-count?hotelId={id}
    // Returns room count from external service (requires authorization)
    [HttpGet("get-room-count")]
    [Authorize]
    public async Task<IActionResult> GetRoomsCount([FromQuery] int hotelId)
    {
        try
        {
            var rooms = await externalMonitoringService.FetchRoomCount(hotelId);

            return Ok(new { count = rooms });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT: api/v1/user/update-admin
    // Updates admin information (requires authorization)
    [HttpPut("update-admin")]
    [Authorize]
    public async Task<IActionResult> UpdateAdmin([FromBody]UpdateUserResource resource)
    {
        try
        {
            var updateUserCommand = UpdateUserCommandFromResourceAssembler.ToCommandFromResource(resource);

            await adminCommandService.Handle(updateUserCommand);
            
            return Ok("User updated correctly!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT: api/v1/user/update-owner
    // Updates owner information (requires authorization)
    [HttpPut("update-owner")]
    [Authorize]
    public async Task<IActionResult> UpdateOwner([FromBody] UpdateUserResource resource)
    {
        try
        {
            var updateUserCommand = UpdateUserCommandFromResourceAssembler.ToCommandFromResource(resource);

            await ownerCommandService.Handle(updateUserCommand);

            return Ok("User updated correctly!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT: api/v1/user/update-worker
    // Updates worker information (requires authorization)
    [HttpPut("update-worker")]
    [Authorize]
    public async Task<IActionResult> UpdateWorker([FromBody] UpdateUserResource resource)
    {
        try
        {
            var updateUserCommand = UpdateUserCommandFromResourceAssembler.ToCommandFromResource(resource);

            await workerCommandService.Handle(updateUserCommand);

            return Ok("User updated correctly!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
