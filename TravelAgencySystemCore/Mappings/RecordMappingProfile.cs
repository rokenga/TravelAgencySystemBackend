using AutoMapper;
using TravelAgencySystemCore.Requests.Record;
using TravelAgencySystemCore.Responses.Record;
using TravelAgencySystemDomain.Entities;

namespace TravelAgencySystemCore.Mappings;

public class RecordMappingProfile : Profile
{
    public RecordMappingProfile()
    {
        CreateMap<RecordRequest, Record>();
        CreateMap<Record, RecordResponse>();
        CreateMap<Record, RecordWithCommentsResponse>();
    }
}