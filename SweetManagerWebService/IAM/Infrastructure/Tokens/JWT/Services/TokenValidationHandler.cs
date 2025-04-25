using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SweetManagerWebService.IAM.Infrastructure.Tokens.JWT.Configuration;

namespace SweetManagerWebService.IAM.Infrastructure.Tokens.JWT.Services;

internal class TokenValidationHandler(IOptions<TokenSettings> tokenSettings) : DelegatingHandler
{
    private readonly TokenSettings _tokenSettings = tokenSettings.Value;

    private static bool TryRetrieveToken(HttpRequestMessage request, out string token)
    {
        token = string.Empty;

        IEnumerable<string>? authHeaders;

        if (!request.Headers.TryGetValues("Authorization", out authHeaders) || authHeaders.Count() > 1)
            return false;

        var bearerToken = authHeaders.ElementAt(0);

        token = bearerToken.StartsWith("Bearer ") ? bearerToken[7..] : bearerToken;

        return true;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        HttpStatusCode statusCode;

        string token;

        if (!TryRetrieveToken(request, out token))
        {
            statusCode = HttpStatusCode.Unauthorized;

            return base.SendAsync(request, cancellationToken);
        }

        try
        {
            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(_tokenSettings.SecretKey));

            SecurityToken securityToken;

            JwtSecurityTokenHandler tokenHandler = new();

            TokenValidationParameters validationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _tokenSettings.Issuer,
                ValidAudience = _tokenSettings.Audience,
                IssuerSigningKey = securityKey,
                LifetimeValidator = LifetimeValidator,
                ClockSkew = TimeSpan.Zero
            };
            Thread.CurrentPrincipal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

            if (securityToken is JwtSecurityToken jwt)
                if (!jwt.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    statusCode = HttpStatusCode.Unauthorized;

            return base.SendAsync(request, cancellationToken);
        }
        catch (SecurityTokenValidationException)
        {
            statusCode = HttpStatusCode.Unauthorized;
        }
        catch (Exception)
        {
            statusCode = HttpStatusCode.InternalServerError;
        }

        return Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(statusCode), cancellationToken);
    }

    private static bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken,
        TokenValidationParameters validationParameters)
    {
        if (expires == null) return false;

        return DateTime.UtcNow < expires;
    }
    
}