using AutoMapper;
using TravelAgencySystemCore.Requests.Destination;
using TravelAgencySystemCore.Responses.Destination;
using TravelAgencySystemDomain.Entities;

namespace TravelAgencySystemCore.Mappings;

public class DestinationMappingProfile : Profile
{
    public DestinationMappingProfile()
    {
        CreateMap<DestinationRequest, Destination>();
        CreateMap<Destination, DestinationResponse>();
        CreateMap<Destination, DestinationWithSpecificRecordAndComments>();
        CreateMap<Destination, DestinationWithAllRecords>();
    }
}