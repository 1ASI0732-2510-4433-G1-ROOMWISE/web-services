using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SweetManagerWebService.Commerce.Domain.Model.Queries.Contracts;
using SweetManagerWebService.Commerce.Domain.Services.Contracts;
using SweetManagerWebService.Commerce.Interfaces.REST.Resources.Contracts;
using SweetManagerWebService.Commerce.Interfaces.REST.Transform.Contracts;
using SweetManagerWebService.IAM.Infrastructure.Pipeline.Middleware.Attributes;

namespace SweetManagerWebService.Commerce.Interfaces.REST;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Authorize]
public class ContractsController(IContractOwnerCommandService contractOwnerCommandService,
    IContractOwnerQueryService contractOwnerQueryService) : ControllerBase
{
    [HttpPost("create-contract-owner")]
    public async Task<IActionResult> CreateContractOwner([FromBody] CreateContractOwnerResource resource)
    {
        try
        {
            var createContractOwnerCommand =
                CreateContractOwnerCommandFromResourceAssembler.ToCommandFromResource(resource);

            await contractOwnerCommandService.Handle(createContractOwnerCommand);

            return Ok("Contract registered correctly!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("get-contract-by-owner-id")]
    public async Task<IActionResult> GetContractByOwnerId([FromQuery] int ownerId)
    {
        try
        {
            var contractOwner = await contractOwnerQueryService.Handle(new GetContractOwnerByOwnerIdQuery(ownerId));

            if (contractOwner is null)
                return BadRequest("There's no contract with the given owner id");
            
            var contractOwnerResource = ContractOwnerResourceFromEntityAssembler.ToResourceFromEntity(contractOwner);

            return Ok(contractOwnerResource);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
}