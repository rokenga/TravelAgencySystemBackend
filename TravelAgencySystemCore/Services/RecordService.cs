using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TravelAgencySystemCore.Interfaces.Repository;
using TravelAgencySystemCore.Interfaces.Service;
using TravelAgencySystemCore.Requests.Record;
using TravelAgencySystemCore.Responses.Comment;
using TravelAgencySystemCore.Responses.Record;
using TravelAgencySystemDomain.Entities;
using TravelAgencySystemDomain.Exceptions;

namespace TravelAgencySystemCore.Services;

public class RecordService : IRecordService
{
    private readonly IRecordRepo _recordRepo;
    private readonly IDestinationRepo _destinationRepo;
    private readonly ICommentRepo _commentRepo;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    
    public RecordService(IRecordRepo recordRepo, IDestinationRepo destinationRepo, ICommentRepo commentRepo, IMapper mapper, UserManager<User> userManager)
    {
        _recordRepo = recordRepo;
        _destinationRepo = destinationRepo;
        _commentRepo = commentRepo;
        _mapper = mapper;
        _userManager = userManager;
    }
    
    public RecordResponse GetRecordById(Guid guid)
    {
        var record = _recordRepo.GetRecordById(guid);
        
        if (record == null)
        {
            throw new NotFoundException($"The destination with id {guid} was not found");
        }
        
        return _mapper.Map<RecordResponse>(record);
    }

    public IEnumerable<Record> GetRecords()
    {
        var recordEntities = _recordRepo.GetRecords();
        var recordResponse = _mapper.Map<IEnumerable<Record>>(recordEntities);
        return recordResponse;
    }

    public Guid AddRecord(RecordRequest record, string agentId)
    {
        var destination = _destinationRepo.DestinationExists(record.DestinationId);
        if (destination == null)
        {
            throw new NotFoundException($"The destination with id {record.DestinationId} was not found");
        }
        var recordEntity = _mapper.Map<Record>(record);
        
        recordEntity.AgentId = agentId;
        
        var agent = _userManager.FindByIdAsync(agentId).Result; 
        if (agent == null)
        {
            throw new NotFoundException($"The agent with id {agentId} was not found");
        }
        recordEntity.Author = agent.Email;
        var res = _recordRepo.AddRecord(recordEntity);
        return res;
    }

    public bool EditRecord(Guid guid, RecordRequest record, string agentId)
    {
        var oldRecordExists = _recordRepo.RecordExists(guid);
        if (!oldRecordExists)
        {
            throw new NotFoundException($"The record with id {guid} was not found");
        }

        var destinationExists = _destinationRepo.DestinationExists(record.DestinationId);
        if (!destinationExists)
        {
            throw new NotFoundException($"The destination with id {record.DestinationId} was not found");
        }

        var oldRecordEntity = _recordRepo.GetRecordById(guid);

        if (oldRecordEntity.AgentId != agentId)
        {
            throw new UnauthorizedAccessException("You are not allowed to edit this record.");
        }
        
        _mapper.Map(record, oldRecordEntity);
        var res = _recordRepo.EditRecord(oldRecordEntity);
        return res;
    }


    public bool DeleteRecord(Guid guid, string agentId)
    {
        var record = _recordRepo.RecordExists(guid);
        if (record == false)
        {
            throw new NotFoundException($"The record with id {guid} was not found");
        }
        
        var recordEntity = _recordRepo.GetRecordById(guid);

        if (recordEntity.AgentId != agentId)
        {
            throw new UnauthorizedAccessException("You are not allowed to delete this record.");
        }
        
        var res = _recordRepo.DeleteRecord(guid);
        return res;
    }

    public RecordWithCommentsResponse GetRecordWithCommentsById(Guid guid)
    {
        var record = _recordRepo.GetRecordById(guid);
        
        if (record == null)
        {
            throw new NotFoundException($"The destination with id {guid} was not found");
        }

        var recordWithCommentsResponse = _mapper.Map<RecordWithCommentsResponse>(record);
        var comments = _commentRepo.GetCommentsForRecordById(guid);
        recordWithCommentsResponse.Comments = _mapper.Map<IEnumerable<CommentResponse>>(comments);
        return recordWithCommentsResponse;
    }
}