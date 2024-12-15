using System.ComponentModel.DataAnnotations;

namespace TravelAgencySystemCore.Requests.Auth;

public class LoginRequest
{
    [EmailAddress]
    public string Email { get; set; }
    public string Password { get; set; }
}