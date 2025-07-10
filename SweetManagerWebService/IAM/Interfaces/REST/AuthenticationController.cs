using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SweetManagerWebService.IAM.Domain.Services.Credential.Admin;
using SweetManagerWebService.IAM.Domain.Services.Credential.Owner;
using SweetManagerWebService.IAM.Domain.Services.Credential.Worker;
using SweetManagerWebService.IAM.Domain.Services.Users.Admin;
using SweetManagerWebService.IAM.Domain.Services.Users.Owner;
using SweetManagerWebService.IAM.Domain.Services.Users.Worker;
using SweetManagerWebService.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using SweetManagerWebService.IAM.Interfaces.REST.Resource.Authentication.User;
using SweetManagerWebService.IAM.Interfaces.REST.Transform.Authentication.User;

namespace SweetManagerWebService.IAM.Interfaces.REST;

[Authorize] // Controller requires authorization by default
[ApiController]
[Route("api/v1/[controller]")] // Base route: /api/v1/authentication
[Produces(MediaTypeNames.Application.Json)] // Returns JSON responses
public class AuthenticationController(IAdminCommandService adminCommandService, 
    IAdminCredentialCommandService adminCredentialCommandService,
    IWorkerCommandService workerCommandService,
    IWorkerCredentialCommandService workerCredentialCommandService,
    IOwnerCommandService ownerCommandService,
    IOwnerCredentialCommandService ownerCredentialCommandService) : ControllerBase
{
    // POST: api/v1/authentication/sign-up-admin
    // Registers a new admin user (anonymous access allowed)
    [HttpPost("sign-up-admin")]
    [AllowAnonymous]
    public async Task<IActionResult> SignUpAdmin([FromBody] SignUpUserResource resource)
    {
        try
        {
            var signUpCommand = SignUpUserCommandFromResourceAssembler.ToCommandFromResource(resource);

            await adminCommandService.Handle(signUpCommand);

            await adminCredentialCommandService.Handle(new(signUpCommand.Id, resource.Password));

            return Ok("User created correctly!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // POST: api/v1/authentication/sign-up-worker
    // Registers a new worker user (anonymous access allowed)
    [HttpPost("sign-up-worker")]
    [AllowAnonymous]
    public async Task<IActionResult> SignUpWorker([FromBody] SignUpUserResource resource)
    {
        try
        {
            var signUpCommand = SignUpUserCommandFromResourceAssembler.ToCommandFromResource(resource);

            await workerCommandService.Handle(signUpCommand);

            await workerCredentialCommandService.Handle(new(resource.Id, resource.Password));

            return Ok("User created correctly!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // POST: api/v1/authentication/sign-up-owner
    // Registers a new owner user (anonymous access allowed)
    [HttpPost("sign-up-owner")]
    [AllowAnonymous]
    public async Task<IActionResult> SignUpOwner([FromBody] SignUpUserResource resource)
    {
        try
        {
            var signUpCommand = SignUpUserCommandFromResourceAssembler.ToCommandFromResource(resource);

            await ownerCommandService.Handle(signUpCommand);

            await ownerCredentialCommandService.Handle(new(resource.Id, resource.Password));

            return Ok("User created correctly!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // POST: api/v1/authentication/sign-in
    // Authenticates a user (anonymous access allowed)
    [HttpPost("sign-in")]
    [AllowAnonymous]
    public async Task<IActionResult> SignIn([FromBody] SignInResource resource)
    {
        try
        {
            if (resource.RolesId is < 1 or > 3)
                throw new Exception();
            
            var signInCommand = SignInCommandFromResourceAssembler.ToCommandFromResource(resource);

            dynamic? authenticatedUser = "";

            if (resource.RolesId is 1)
                authenticatedUser = await ownerCommandService.Handle(signInCommand);
            else if (resource.RolesId is 2)
                authenticatedUser = await adminCommandService.Handle(signInCommand);
            else if(resource.RolesId is 3)
                authenticatedUser = await workerCommandService.Handle(signInCommand);

            var authenticatedUserResource =
                AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(authenticatedUser!.User,
                    authenticatedUser.Token);

            return Ok(authenticatedUserResource);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
