namespace TravelAgencySystemDomain.Entities;

public class Record : BaseEntity
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public Guid DestinationId { get; set; }
    public Destination Destination { get; set; }

    public ICollection<Comment> comments { get; set; }
    
    public String AgentId { get; set; }
    
    public User Agent { get; set; }
}