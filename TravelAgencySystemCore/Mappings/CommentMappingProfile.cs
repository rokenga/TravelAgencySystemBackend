using AutoMapper;
using TravelAgencySystemCore.Requests.Comment;
using TravelAgencySystemCore.Responses.Comment;
using TravelAgencySystemDomain.Entities;

namespace TravelAgencySystemCore.Mappings;

public class CommentMappingProfile : Profile
{
    public CommentMappingProfile()
    {
        CreateMap<CommentRequest, Comment>();
        CreateMap<Comment, CommentResponse>();
    }
}