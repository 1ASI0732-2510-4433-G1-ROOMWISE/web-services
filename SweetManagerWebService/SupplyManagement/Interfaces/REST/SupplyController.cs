using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SweetManagerWebService.SupplyManagement.Domain.Model.Queries;
using SweetManagerWebService.SupplyManagement.Domain.Services;
using SweetManagerWebService.SupplyManagement.Interfaces.REST.Resources;
using SweetManagerWebService.SupplyManagement.Interfaces.REST.Transform;

namespace SweetManagerWebService.SupplyManagement.Interfaces.REST
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class SupplyController(ISupplyCommandService supplyCommandService, ISupplyQueryService supplyQueryService) : ControllerBase
    {

        [HttpPost("create-supply")]
        public async Task<IActionResult> CreateSupply([FromBody] CreateSupplyResource resource)
        {
            try
            {
                var result = await supplyCommandService.Handle(CreateSupplyCommandFromResourceAssembler.ToCommandFromResource(resource));
                if (!result)
                {
                    return BadRequest("Failed to create supply.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupply(int id, [FromBody] UpdateSupplyResource resource)
        {
            try
            {
                // Validate that the provided resource is valid
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // The `id` is passed via the route, so we don't pass it in the body
                var result = await supplyCommandService.Handle(UpdateSupplyCommandFromResource.FromResource(id, resource));

                if (!result)
                {
                    return BadRequest("Failed to update supply.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-supply")]
        public async Task<IActionResult> DeleteSupply([FromBody] DeleteSupplyResource resource)
        {
            try
            {
                var result = await supplyCommandService.Handle(DeleteSupplyCommandFromResourceAssembler.ToCommandFromResource(resource));
                if (!result)
                {
                    return BadRequest("Failed to delete supply.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplyById([FromRoute] int id)
        {
            try
            {
                var result = await supplyQueryService.Handle(new GetSupplyByIdQuery(id));
                if (result == null)
                {
                    return NotFound($"Supply with ID {id} not found.");
                }

                var supplyResource = SupplyResourceFromEntityAssembler.ToResourceFromEntity(result);

                return Ok(supplyResource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-all-supplies")]
        public async Task<IActionResult> GetAllSupplies([FromQuery] int hotelId)
        {
            try
            {
                // Pass HotelId into the query
                var result = await supplyQueryService.Handle(new GetAllSuppliesQuery(hotelId));

                // Map the result to a collection of supply resources
                var supplyResource = result.Select(SupplyResourceFromEntityAssembler.ToResourceFromEntity);

                return Ok(supplyResource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("provider/{providerId}")]

        public async Task<IActionResult> GetSupplyByProviderId([FromRoute] int providerId)
        {
            try
            {
                var result = await supplyQueryService.Handle(new GetSupplyByProviderIdQuery(providerId));
                if (result == null)
                {
                    return NotFound($"Supply with Provider ID {providerId} not found.");
                }

                var supplyResource = result.Select(SupplyResourceFromEntityAssembler.ToResourceFromEntity);

                return Ok(supplyResource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
