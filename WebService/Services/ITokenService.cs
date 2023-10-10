namespace WebService.Services
{
    public interface ITokenService
    {
        // Method to revoke a token
        void RevokeToken(string token);

        // Method to check if a token is revoked
        bool IsTokenRevoked(string token);
    }
}
