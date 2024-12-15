using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TravelAgencySystemCore.Interfaces.Repository;
using TravelAgencySystemCore.Interfaces.Service;
using TravelAgencySystemCore.Requests.Comment;
using TravelAgencySystemCore.Responses.Comment;
using TravelAgencySystemDomain.Entities;
using TravelAgencySystemDomain.Exceptions;

namespace TravelAgencySystemCore.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepo _commentRepo;
    private readonly IRecordRepo _recordRepo;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    
    public CommentService(ICommentRepo commentRepo, IRecordRepo recordRepo, IMapper mapper, UserManager<User> userManager)
    {
        _commentRepo = commentRepo;
        _recordRepo = recordRepo;
        _mapper = mapper;
        _userManager = userManager;
    }
    public CommentResponse GetCommentById(Guid guid)
    {
        var comment = _commentRepo.GetCommentById(guid);
        
        if (comment == null)
        {
            throw new NotFoundException($"The comment with id {guid} was not found");
        }
        
        return _mapper.Map<CommentResponse>(comment);
    }

    public IEnumerable<Comment> GetComments()
    {
        var commentEntities = _commentRepo.GetComments();
        var commentResponse = _mapper.Map<IEnumerable<Comment>>(commentEntities);
        return commentResponse;
    }

    public Guid AddComment(CommentRequest comment, string clientId)
    {
        var record = _recordRepo.RecordExists(comment.RecordId);
        if (record == false)
        {
            throw new NotFoundException($"The record with id {comment.RecordId} was not found");
        }
        var commentEntity = _mapper.Map<Comment>(comment);
        
        commentEntity.ClientID = clientId;
        
        var client = _userManager.FindByIdAsync(clientId).Result; 
        if (client == null)
        {
            throw new NotFoundException($"The agent with id {client} was not found");
        }
        commentEntity.Author = client.Email;
        
        var res = _commentRepo.AddComment(commentEntity);
        return res;
    }

    public bool EditComment(Guid guid, CommentRequest comment, string clientId)
    {
        var oldComment = _commentRepo.CommentExists(guid);
        if (!oldComment)
        {
            throw new NotFoundException($"The record with id {guid} was not found");
        }
    
        var record = _recordRepo.RecordExists(comment.RecordId);
        if (!record)
        {
            throw new NotFoundException($"The record with id {comment.RecordId} was not found");
        }

        var oldCommentEntity = _commentRepo.GetCommentById(guid);

        if (oldCommentEntity.ClientID != clientId)
        {
            throw new UnauthorizedAccessException("You are not allowed to edit this comment.");
        }
        
        _mapper.Map(comment, oldCommentEntity);
        var res = _commentRepo.EditComment(oldCommentEntity);
        return res;
    }


    public bool DeleteComment(Guid guid, string clientId)
    {
        var comment = _commentRepo.CommentExists(guid);
        if (comment == false)
        {
            throw new NotFoundException($"The record with id {guid} was not found");
        }
        
        var commentEntity = _commentRepo.GetCommentById(guid);

        if (commentEntity.ClientID != clientId)
        {
            throw new UnauthorizedAccessException("You are not allowed to delete this comment.");
        }
        
        var res = _commentRepo.DeleteComment(guid);
        return res;
    }
}