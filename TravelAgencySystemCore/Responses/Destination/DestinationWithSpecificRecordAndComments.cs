using TravelAgencySystemCore.Responses.Record;

namespace TravelAgencySystemCore.Responses.Destination;

public class DestinationWithSpecificRecordAndComments
{
    public Guid Id { get; set; }
    public string Country { get; set; } 
    public string City { get; set; } 
    public string Description { get; set; } 
    public RecordWithCommentsResponse RecordWithCommentsResponse { get; set; }
}