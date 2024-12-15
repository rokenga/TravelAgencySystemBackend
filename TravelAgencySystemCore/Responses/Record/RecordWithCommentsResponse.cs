using TravelAgencySystemCore.Responses.Comment;

namespace TravelAgencySystemCore.Responses.Record;

public class RecordWithCommentsResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Content { get; set; }
    public Guid DestinationId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public IEnumerable<CommentResponse> Comments { get; set; }
}