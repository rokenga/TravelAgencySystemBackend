using AutoMapper;
using TravelAgencySystemCore.Interfaces.Repository;
using TravelAgencySystemCore.Interfaces.Service;
using TravelAgencySystemCore.Requests.Destination;
using TravelAgencySystemCore.Responses.Comment;
using TravelAgencySystemCore.Responses.Destination;
using TravelAgencySystemCore.Responses.Record;
using TravelAgencySystemDomain.Entities;
using TravelAgencySystemDomain.Exceptions;

namespace TravelAgencySystemCore.Services;

public class DestinationService : IDestinationService
{
    private readonly IDestinationRepo _destinationRepo;
    private readonly IRecordRepo _recordRepo;
    private readonly ICommentRepo _commentRepo;
    
    private readonly IMapper _mapper;

    public DestinationService(IDestinationRepo destinationRepo, IRecordRepo recordRepo, ICommentRepo commentRepo, IMapper mapper)
    {
        _destinationRepo = destinationRepo;
        _recordRepo = recordRepo;
        _commentRepo = commentRepo;
        _mapper = mapper;
    }
    
    public DestinationResponse GetDestinationById(Guid guid)
    {
        var destination = _destinationRepo.GetDestinationById(guid);
        
        if (destination == null)
        {
            throw new NotFoundException($"The destination with id {guid} was not found");
        }
        
        return _mapper.Map<DestinationResponse>(destination);
    }

    public IEnumerable<Destination> GetDestinations()
    {
        var destinationEntities = _destinationRepo.GetDestinations();
        var destinationResponse = _mapper.Map<IEnumerable<Destination>>(destinationEntities);
        return destinationResponse;
    }

    public Guid AddDestination(DestinationRequest destination)
    {
        var destinationEntity = _mapper.Map<Destination>(destination);
        var res = _destinationRepo.AddDestination(destinationEntity);
        return res;
    }

    public bool EditDestination(Guid guid, DestinationRequest destination)
    {
        var oldDestination = _destinationRepo.DestinationExists(guid);
        if (oldDestination == false)
        {
            throw new NotFoundException($"The destination with id {guid} was not found");
        }
        var newDestination = _mapper.Map<Destination>(destination);
        newDestination.Id = guid;
        var res = _destinationRepo.EditDestination(newDestination);
        return res;
    }

    public bool DeleteDestination(Guid guid)
    {
        var destination = _destinationRepo.GetDestinationById(guid);
        if (destination == null)
        {
            throw new NotFoundException($"The destination with id {guid} was not found");
        }
        var res = _destinationRepo.DeleteDestination(guid);
        return res;
    }

    public DestinationWithSpecificRecordAndComments GetDestinationWithSpecificRecordAndComments(Guid recordId, Guid destinationId)
    {
        var destinationEntity = _destinationRepo.GetDestinationById(destinationId);
        if (destinationEntity == null)
        {
            throw new NotFoundException($"Destination with Id: {destinationId} was not found");
        }
        
        var recordEntity = _recordRepo.GetRecordById(recordId);
        if (recordEntity == null)
        {
            throw new NotFoundException($"Route with Id: {recordId} was not found");
        }

        if (destinationEntity.Id != recordEntity.DestinationId)
        {
            throw new NotFoundException($"Hike {destinationId} does not contain {recordId} route");
        }

        var commentResponse = _commentRepo.GetCommentsForRecordById(recordId);
        var destinationResponse = _mapper.Map<DestinationWithSpecificRecordAndComments>(destinationEntity);
        destinationResponse.RecordWithCommentsResponse = _mapper.Map<RecordWithCommentsResponse>(recordEntity);
        destinationResponse.RecordWithCommentsResponse.Comments = _mapper.Map<IEnumerable<CommentResponse>>(commentResponse);
        
        return destinationResponse;
    }

    public DestinationWithAllRecords GetDestinationWithAllRecords(Guid destinationId)
    {
        var destinationEntity = _destinationRepo.GetDestinationById(destinationId);
        if (destinationEntity == null)
        {
            throw new NotFoundException($"Destination with Id: {destinationId} was not found");
        }

        var recordEntities = _recordRepo.GetRecordsForDestinationById(destinationId);

        var destinationResponse = _mapper.Map<DestinationWithAllRecords>(destinationEntity);

        destinationResponse.Records = _mapper.Map<IEnumerable<RecordResponse>>(recordEntities);

        return destinationResponse;
    }

}