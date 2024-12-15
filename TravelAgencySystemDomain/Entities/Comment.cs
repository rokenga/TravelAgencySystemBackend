namespace TravelAgencySystemDomain.Entities;

public class Comment : BaseEntity
{
    public string Author { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid RecordId { get; set; }
    public Record Record { get; set; }
    public String ClientID { get; set; }
    public User Client { get; set; }
}