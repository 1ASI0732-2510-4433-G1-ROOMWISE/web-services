using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SweetManagerWebService.Commerce.Domain.Model.Queries.Subscriptions;
using SweetManagerWebService.Commerce.Domain.Services.Subscriptions;
using SweetManagerWebService.Commerce.Interfaces.REST.Resources.Subscriptions;
using SweetManagerWebService.Commerce.Interfaces.REST.Transform.Subscriptions;
using SweetManagerWebService.IAM.Infrastructure.Pipeline.Middleware.Attributes;

namespace SweetManagerWebService.Commerce.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Authorize]
public class SubscriptionController(ISubscriptionCommandService subscriptionCommandService, 
    ISubscriptionQueryService subscriptionQueryService): ControllerBase
{
    [HttpPost("create-subscription")]
    public async Task<IActionResult> CreateSubscription([FromBody] CreateSubscriptionResource resource)
    {
        try
        {
            var command = CreateSubscriptionCommandFromResourceAssembler.ToCommandFromResource(resource);

            await subscriptionCommandService.Handle(command);
            
            return Ok("Subscription created correctly!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("get-subscriptions")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllSubscriptions()
    {
        try
        {
            var subscriptions = await subscriptionQueryService.Handle(new GetAllSubscriptionsQuery());

            var subscriptionResources 
                = subscriptions.Select(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity);
            
            return Ok(subscriptionResources);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("get-subscription-id")]
    [AllowAnonymous]
    public async Task<IActionResult> GetSubscriptionById([FromQuery] int id)
    {
        try
        {
            var subscription = await subscriptionQueryService.Handle(new GetSubscriptionByIdQuery(id));

            if (subscription is null)
                return BadRequest("There's no subscription with the given Id");
            
            var subscriptionResource = SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription);

            return Ok(subscriptionResource);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
}