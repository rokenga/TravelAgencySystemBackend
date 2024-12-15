using System.Security.Claims;

namespace TravelAgencySystemCore.Interfaces.Service;

public interface IJwtTokenService
{
    public string CreateAccessToken(string email, string userId, IEnumerable<string> userRoles);
    public string CreateRefreshToken();
    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}