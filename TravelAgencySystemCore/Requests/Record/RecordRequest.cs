using System.ComponentModel.DataAnnotations;

namespace TravelAgencySystemCore.Requests.Record;

public class RecordRequest
{
    [Required(ErrorMessage = "The Title field cannot be empty")]
    public string Title { get; set; }
    [Required(ErrorMessage = "The Content field cannot be empty")]
    public string Content { get; set; }
    [Required(ErrorMessage = "The DestinationId field cannot be empty")]
    public Guid DestinationId { get; set; }
}