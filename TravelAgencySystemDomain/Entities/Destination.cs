namespace TravelAgencySystemDomain.Entities;

public class Destination : BaseEntity
{
    public string Country { get; set; } 
    public string City { get; set; } 
    public string Description { get; set; } 
    public ICollection<Record> records { get; set; }
}