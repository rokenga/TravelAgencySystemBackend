using TravelAgencySystemCore.Requests.Destination;
using TravelAgencySystemCore.Responses.Destination;
using TravelAgencySystemDomain.Entities;

namespace TravelAgencySystemCore.Interfaces.Service;

public interface IDestinationService
{
    DestinationResponse GetDestinationById(Guid guid);
    IEnumerable<Destination> GetDestinations();
    Guid AddDestination(DestinationRequest destination);
    bool EditDestination(Guid guid, DestinationRequest destination);
    bool DeleteDestination(Guid guid);
    DestinationWithSpecificRecordAndComments GetDestinationWithSpecificRecordAndComments(Guid recordId, Guid destinationId);
    DestinationWithAllRecords GetDestinationWithAllRecords(Guid destinationId);
}