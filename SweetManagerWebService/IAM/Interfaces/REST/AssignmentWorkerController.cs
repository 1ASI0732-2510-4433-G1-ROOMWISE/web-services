using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SweetManagerWebService.IAM.Domain.Model.Queries;
using SweetManagerWebService.IAM.Domain.Services.Assignments;
using SweetManagerWebService.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using SweetManagerWebService.IAM.Interfaces.REST.Resource.Assignments;
using SweetManagerWebService.IAM.Interfaces.REST.Transform.Assignments;

namespace SweetManagerWebService.IAM.Interfaces.REST;


[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class AssignmentWorkerController(IAssignmentWorkerCommandService assignmentWorkerCommandService,
    IAssignmentWorkerQueryService assignmentWorkerQueryService) : ControllerBase
{
    [HttpPost("create-assignment-worker")]
    [Authorize]
    public async Task<IActionResult> CreateAssignmentWorker([FromBody] CreateAssignmentWorkerResource resource)
    {
        try
        {
            var createAssignmentWorkerCommand =
                CreateAssignmentWorkerCommandFromResourceAssembler.ToCommandFromResource(resource);

            await assignmentWorkerCommandService.Handle(createAssignmentWorkerCommand);

            return Ok("Assignment worker created correctly!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("get-assignment-by-worker-id")]
    [Authorize]
    public async Task<IActionResult> GetAssignmentWorkersByWorkerId([FromQuery] int id)
    {
        try
        {
            var assignmentWorker =
                await assignmentWorkerQueryService.Handle(new GetAssignmentWorkerByWorkerIdQuery(id));

            var assignmentWorkerResource =
                assignmentWorker.Select(AssignmentWorkerResourceFromEntityAssembler.ToResourceFromEntity);

            return Ok(assignmentWorkerResource);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("get-all-assignments-by-admin-id")]
    [Authorize]
    public async Task<IActionResult> GetAssignmentWorkersByAdminId([FromQuery]int id)
    {
        try
        {
            var admins 
                = await assignmentWorkerQueryService.Handle(new GetAssignmentWorkerByAdminIdQuery(id));

            var adminsResource 
                = admins.Select(AssignmentWorkerResourceFromEntityAssembler.ToResourceFromEntity);
            
            return Ok(adminsResource);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("get-assignments-by-workers-area-id")]
    [Authorize]
    public async Task<IActionResult> GetAssignmentWorkersByWorkerAreaId([FromQuery] int id)
    {
        try
        {
            var assignmentWorkers
                = await assignmentWorkerQueryService.Handle(
                    new GetAssignmentWorkerByWorkerAreaIdQuery(id));

            var assignmentWorkerResources =
                assignmentWorkers.Select(AssignmentWorkerResourceFromEntityAssembler.ToResourceFromEntity);

            return Ok(assignmentWorkerResources);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
}