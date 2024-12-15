using TravelAgencySystemDomain.Entities;

namespace TravelAgencySystemCore.Interfaces.Repository;

public interface IDestinationRepo
{
    Destination GetDestinationById(Guid id);
    IEnumerable<Destination> GetDestinations();
    Guid AddDestination(Destination destination);
    bool EditDestination(Destination destination);
    bool DeleteDestination(Guid guid);
    bool DestinationExists(Guid guid);
    
}