using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SweetManagerWebService.IAM.Infrastructure.Pipeline.Middleware.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute(params string[]? roles) : Attribute , IAuthorizationFilter
{
    private readonly string[]? _listRoles = roles ?? [];
    
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();

        if (allowAnonymous) return;

        var credential = context.HttpContext.Items["Credentials"] as dynamic;

        if (credential is null)
        {
            context.Result = new UnauthorizedResult();

            return;
        }

        if (_listRoles != null && (_listRoles.Length <= 0 || HasRequiredRole(credential.Role))) return;

        context.Result = new ForbidResult();
    }
    
    private bool HasRequiredRole(string role) => _listRoles != null && _listRoles.Contains(role);
    
}