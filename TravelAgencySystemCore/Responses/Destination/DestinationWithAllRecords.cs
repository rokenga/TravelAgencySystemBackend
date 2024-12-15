using TravelAgencySystemCore.Responses.Record;

namespace TravelAgencySystemCore.Responses.Destination;

public class DestinationWithAllRecords
{
    public Guid Id { get; set; }
    public string Country { get; set; } 
    public string City { get; set; } 
    public string Description { get; set; } 
    
    public IEnumerable<RecordResponse> Records { get; set; }
}