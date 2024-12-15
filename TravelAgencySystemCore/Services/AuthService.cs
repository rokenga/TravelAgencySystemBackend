using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using TravelAgencySystemCore.Interfaces.Service;
using TravelAgencySystemCore.Requests.Auth;
using TravelAgencySystemCore.Responses.Auth;
using TravelAgencySystemDomain.Entities;
using TravelAgencySystemDomain.Exceptions;

namespace TravelAgencySystemCore.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IConfiguration _configuration;
    
    public AuthService(IJwtTokenService jwtTokenService, UserManager<User> userManager, IConfiguration configuration)
    {
        _jwtTokenService = jwtTokenService;
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<SuccessfulLoginResponse> Login(LoginRequest loginRequest)
    {
        var user = await _userManager.FindByEmailAsync(loginRequest.Email);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
        if (!isPasswordValid)
        {
            throw new NotFoundException("User name or password is invalid.");
        }

        var roles = await _userManager.GetRolesAsync(user);
        var accessToken = _jwtTokenService.CreateAccessToken(user.Email, user.Id, roles);

        await UpdateUsersRefreshTokenWithExpiration(user);

        return new SuccessfulLoginResponse()
        {
            AccessToken = accessToken, 
            RefreshToken = user.RefreshToken
        };
    }

    public async Task<SuccessfulLoginResponse> Register(RegisterRequest registerRequest)
    {
        var userTaken = await _userManager.FindByEmailAsync(registerRequest.Email);

        if (userTaken != null)
        {
            throw new NotFoundException("Invalid username or password");
        }
        //Todo: Add to automapper
        var newUser = new User()
        {
            Email = registerRequest.Email,
            UserName = registerRequest.Email
        };

        var createUserResult = await _userManager.CreateAsync(newUser, registerRequest.Password);

        if (createUserResult.Succeeded)
        {
            await _userManager.AddToRoleAsync(newUser, UserRoles.Client);
            var roles = await _userManager.GetRolesAsync(newUser);

            var accessToken = _jwtTokenService.CreateAccessToken(newUser.Email, newUser.Id, roles);
            var createdUser = await _userManager.FindByEmailAsync(newUser.Email);

            await UpdateUsersRefreshTokenWithExpiration(createdUser);

            return new SuccessfulLoginResponse()
            {
                AccessToken = accessToken, 
                RefreshToken = createdUser.RefreshToken
            };
        }

        throw new NotFoundException(createUserResult.Errors.First().Description);
    }

    public async Task<SuccessfulLoginResponse> RefreshToken(RefreshTokenRequest refreshTokenRequest)
    {
        ClaimsPrincipal? principal;
        try
        {
            principal = _jwtTokenService.GetPrincipalFromExpiredToken(refreshTokenRequest.AccessToken);
        }
        catch
        {
            throw new NotFoundException("Invalid token");
        }

        var userID = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userID);
        var isRefreshTokenInvalid = user.RefreshToken != refreshTokenRequest.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow;

        if (user == null || isRefreshTokenInvalid)
        {
            throw new NotFoundException("Invalid refresh token");
        }

        var roles = await _userManager.GetRolesAsync(user);
        var accessToken = _jwtTokenService.CreateAccessToken(user.Email, user.Id, roles);
        await UpdateUsersRefreshTokenWithExpiration(user);

        return new SuccessfulLoginResponse()
        {
            AccessToken = accessToken, 
            RefreshToken = user.RefreshToken
        };
    }

    public async Task<UserResponse> GetUser(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }

        var roles = await _userManager.GetRolesAsync(user);
        var role = roles.FirstOrDefault() ?? "No Role Assigned";

        var userResponse = new UserResponse
        {
            Id = user.Id,
            Email = user.Email,
            Role = role
        };

        return userResponse;
    }


    private async Task UpdateUsersRefreshTokenWithExpiration(User? user)
    {
        int refreshTokenValidityDays = int.Parse(_configuration["JWT:RefreshTokenValidityDays"]);

        user.RefreshToken = _jwtTokenService.CreateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(refreshTokenValidityDays);

        await _userManager.UpdateAsync(user);
    }
}