namespace WebService.Services
{
    public class TokenService : ITokenService
    {
        private readonly List<string> _revokedTokens = new List<string>();

        // Method to revoke a token
        public void RevokeToken(string token)
        {
            _revokedTokens.Add(token);
        }

        // Method to check if a token is revoked
        public bool IsTokenRevoked(string token)
        {
            return _revokedTokens.Contains(token);
        }
    }

}
