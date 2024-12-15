using System.ComponentModel.DataAnnotations;

namespace TravelAgencySystemCore.Requests.Destination;

public class DestinationRequest
{
    [Required(ErrorMessage = "The Country field cannot be empty")]
    public string Country { get; set; } 
    [Required(ErrorMessage = "The City field cannot be empty")]
    public string City { get; set; } 
    [Required(ErrorMessage = "The Description field cannot be empty")]
    public string Description { get; set; } 
}