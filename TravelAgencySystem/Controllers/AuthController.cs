using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelAgencySystemCore.Helpers.Auth;
using TravelAgencySystemCore.Interfaces.Service;
using TravelAgencySystemCore.Requests.Auth;

namespace TravelAgencySystem.Controllers;

public class AuthController : BaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterRequest registerRequest)
    {
        return Ok(await _authService.Register(registerRequest));
    }
    
    [AllowAnonymous]
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginRequest loginRequest)
    {
        return Ok(await _authService.Login(loginRequest));
    }
    
    [HttpPost]
    [Route("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenRequest refreshRequest)
    {
        return Ok(await _authService.RefreshToken(refreshRequest));
    }

    [HttpGet]
    [Route("getUser")]
    [Authorize(Policy = PolicyNames.ClientRole)]
    public async Task<IActionResult> GetUser()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("User ID not found in token.");
        }
        return Ok(await _authService.GetUser(userId));
    }
}