using Microsoft.AspNetCore.Identity;
namespace TravelAgencySystemDomain.Entities;

public class User : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    
    public ICollection<Comment> comments { get; set; }
    public ICollection<Record> records { get; set; }
}