namespace SweetManagerWebService.IAM.Application.Internal.OutboundServices;

public interface IHashingService
{
    string CreateSalt();

    string HashCode(string code, string salt);

    public bool VerifyHash(string code, string salt, string hash);
}