using System.ComponentModel.DataAnnotations;

namespace TravelAgencySystemCore.Requests.Auth;

public class RegisterRequest
{
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}