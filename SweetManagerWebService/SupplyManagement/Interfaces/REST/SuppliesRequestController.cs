using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SweetManagerWebService.SupplyManagement.Domain.Model.Queries;
using SweetManagerWebService.SupplyManagement.Domain.Services;
using SweetManagerWebService.SupplyManagement.Interfaces.REST.Resources;
using SweetManagerWebService.SupplyManagement.Interfaces.REST.Transform;

namespace SweetManagerWebService.SupplyManagement.Interfaces.REST;

[Route("api/[controller]")]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
public class SuppliesRequestController : ControllerBase
{
    private readonly ISuppliesRequestQueryService _queryService;
    private readonly ISuppliesRequestCommandService _commandService;

    public SuppliesRequestController(ISuppliesRequestQueryService queryService, ISuppliesRequestCommandService commandService)
    {
        _queryService = queryService;
        _commandService = commandService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSuppliesRequest([FromBody] CreateSuppliesRequestResource resource)
    {
        try
        {
            var result = await _commandService.Handle(CreateSuppliesRequestCommandFromResourceAssembler.ToCommandFromResource(resource));
            if (!result)
            {
                return BadRequest("Failed to create supplies request.");
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }

    [HttpGet("HotelId/{HotelId}")]
    public async Task<IActionResult> GetAllSuppliesRequest([FromRoute] int HotelId)
    {
        try
        {
            var result = await _queryService.Handle(new GetAllSuppliesRequestQuery(HotelId));
    
            if (result == null || !result.Any())
            {
                return BadRequest("No supplies requests found for this hotel.");
            }

            var suppliesRequestResources = result.Select(SuppliesRequestResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(suppliesRequestResources);
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSuppliesRequestById([FromRoute] int id)
    {
        try
        {
            var result = await _queryService.Handle(new GetSuppliesRequestByIdQuery(id));
            if (result is null)
            {
                return BadRequest($"Supplies request with ID {id} not found.");
            }

            var suppliesRequestResource = SuppliesRequestResourceFromEntityAssembler.ToResourceFromEntity(result);
            return Ok(suppliesRequestResource);
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }
    
    [HttpGet("paymentOwner/{paymentOwnerId}")]
    public async Task<IActionResult> GetSuppliesRequestByPaymentOwnerId([FromRoute] int paymentOwnerId)
    {
        try
        {
            var result = await _queryService.Handle(new GetSuppliesRequestByPaymentOwnerIdQuery(paymentOwnerId));
            if (result is null)
            {
                return BadRequest($"No supplies requests found for PaymentOwnerId {paymentOwnerId}.");
            }

            var suppliesRequestResource = SuppliesRequestResourceFromEntityAssembler.ToResourceFromEntity(result);
            return Ok(suppliesRequestResource);
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }
    
    [HttpGet("supply/{supplyId}")]
    public async Task<IActionResult> GetSuppliesRequestBySupplyId([FromRoute] int supplyId)
    {
        try
        {
            var result = await _queryService.Handle(new GetSuppliesRequestBySupplyIdQuery(supplyId));
            if (result is null)
            {
                return BadRequest($"No supplies requests found for SupplyId {supplyId}.");
            }

            var suppliesRequestResource = SuppliesRequestResourceFromEntityAssembler.ToResourceFromEntity(result);
            return Ok(suppliesRequestResource);
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }
}
