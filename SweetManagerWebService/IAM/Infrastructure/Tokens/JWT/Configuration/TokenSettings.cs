namespace SweetManagerWebService.IAM.Infrastructure.Tokens.JWT.Configuration;

public class TokenSettings
{
    public string SecretKey { get; set; }
    
    public string Audience { get; set; }
    
    public string Issuer { get; set;  }
    
    public int Expire { get; set; }

    public TokenSettings()
    {
        SecretKey = string.Empty;
        Audience = string.Empty;
        Issuer = string.Empty;
        Expire = 0;
    }
}