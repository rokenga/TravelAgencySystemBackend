namespace TravelAgencySystemCore.Responses.Comment;

public class CommentResponse
{
    public Guid Id { get; set; }
    public string Author { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid RecordId { get; set; }
}