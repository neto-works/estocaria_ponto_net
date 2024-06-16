using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EstocariaNet.Services.Interfaces
{
    public interface ITokenServices
    {
        public JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims,IConfiguration _config);
        public string GenerateRefreshToken();
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration _config);
    }
}
