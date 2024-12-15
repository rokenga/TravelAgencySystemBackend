using System.ComponentModel.DataAnnotations;

namespace TravelAgencySystemCore.Requests.Comment;

public class CommentRequest
{
    [Required(ErrorMessage = "The Text field cannot be empty")]
    public string Text { get; set; }
    [Required(ErrorMessage = "The RecordId field cannot be empty")]
    public Guid RecordId { get; set; }
}