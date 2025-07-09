using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SweetManagerWebService.IAM.Infrastructure.Pipeline.Middleware.Attributes;

// Custom attribute to authorize users based on their roles
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute(params string[]? roles) : Attribute, IAuthorizationFilter
{
    // List of roles required to access the resource, provided during initialization
    private readonly string[]? _listRoles = roles ?? [];
    
    // Method to perform authorization check based on roles
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // Check if the action allows anonymous access
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();

        // If anonymous access is allowed, skip authorization check
        if (allowAnonymous) return;

        // Retrieve the user credentials from the HTTP context
        var credential = context.HttpContext.Items["Credentials"] as dynamic;

        // If credentials are not found, return an Unauthorized response
        if (credential is null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        // If roles are defined and the user doesn't have the required role, return a Forbidden response
        if (_listRoles != null && (_listRoles.Length <= 0 || HasRequiredRole(credential.Role))) return;

        context.Result = new ForbidResult();
    }
    
    // Helper method to check if the user has one of the required roles
    private bool HasRequiredRole(string role) => _listRoles != null && _listRoles.Contains(role);
}
