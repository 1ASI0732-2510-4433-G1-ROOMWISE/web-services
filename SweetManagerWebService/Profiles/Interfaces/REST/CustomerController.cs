using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SweetManagerWebService.Profiles.Domain.Model.Commands.Customer;
using SweetManagerWebService.Profiles.Domain.Model.Queries.Customer;
using SweetManagerWebService.Profiles.Domain.Services.Customer;
using SweetManagerWebService.Profiles.Interfaces.REST.Resources.Customer;
using SweetManagerWebService.Profiles.Interfaces.REST.Transform.Customer;

namespace SweetManagerWebService.Profiles.Interfaces.REST;


[Route("api/[controller]")]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
public class CustomerController : ControllerBase
{
    private readonly ICustomerCommandService _customerCommandService;
    private readonly ICustomerQueryService _customerQueryService;

    public CustomerController(ICustomerCommandService customerCommandService, ICustomerQueryService customerQueryService)
    {
        _customerCommandService = customerCommandService;
        _customerQueryService = customerQueryService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerResource resource)
    {
        try
        {
            var result = await _customerCommandService
                .Handle(CreateCustomerCommandFromResourceAssembler
                    .ToCommandFromResource(resource));
                
            if (result is false)
                return BadRequest("Failed to create customer.");
        
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception (ex) details here for debugging
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerResource resource)
    {
        var result = await _customerCommandService
            .Handle(UpdateCustomerCommandFromResourceAssembler
                .ToCommandFromResource(resource));
        if (result is false)
            return BadRequest();
        return Ok(result);
    }
    
    [HttpGet("get-all-customers/{hotelId:int}")]
    public async Task<IActionResult> AllCustomers(int hotelId)
    {
        var customer = await _customerQueryService
            .Handle(new GetAllCustomersQuery(hotelId));

        var customerResource = customer.Select
        (CustomerResourceFromEntityAssembler
            .ToResourceFromEntity);

        return Ok(customerResource);
    }
}
