using TravelAgencySystemCore.Requests.Auth;
using TravelAgencySystemCore.Responses.Auth;

namespace TravelAgencySystemCore.Interfaces.Service;

public interface IAuthService
{
    public Task<SuccessfulLoginResponse> Login(LoginRequest loginRequest);
    public Task<SuccessfulLoginResponse> Register(RegisterRequest registerRequest);
    public Task<SuccessfulLoginResponse> RefreshToken(RefreshTokenRequest refreshTokenRequest);
    public Task<UserResponse> GetUser(string userId);
}