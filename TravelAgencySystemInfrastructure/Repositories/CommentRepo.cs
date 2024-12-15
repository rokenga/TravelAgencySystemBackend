using Microsoft.EntityFrameworkCore;
using TravelAgencySystemCore.Interfaces.Repository;
using TravelAgencySystemDomain.Entities;
using TravelAgencySystemInfrastructure.Data;

namespace TravelAgencySystemInfrastructure.Repositories;

public class CommentRepo : ICommentRepo
{
    private readonly TravelAgencySystemDataContext _dbContext;

    public CommentRepo(TravelAgencySystemDataContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Comment GetCommentById(Guid id)
    {
        return _dbContext.Comments.FirstOrDefault(u => u.Id == id);
    }

    public IEnumerable<Comment> GetComments()
    {
        IEnumerable<Comment> comments = _dbContext.Comments;
        return comments.ToList();
    }

    public Guid AddComment(Comment comment)
    {
        comment.Id = Guid.NewGuid();
        comment.CreatedAt = DateTime.Now;
        comment.UpdatedAt = DateTime.Now;
        _dbContext.Comments.Add(comment);
        _dbContext.SaveChanges();
        
        return comment.Id;
    }

    public bool EditComment(Comment comment)
    {
        comment.UpdatedAt = DateTime.Now;
        _dbContext.Comments.Update(comment);
        _dbContext.SaveChanges();
        return true;
    }

    public bool DeleteComment(Guid guid)
    {
        _dbContext.Comments
            .Where(d => d.Id == guid).ExecuteDelete();
        
        _dbContext.SaveChanges();
        return true;
    }

    public bool CommentExists(Guid guid)
    {
        return _dbContext.Comments.Any(d => d.Id == guid);
    }
    
    public IEnumerable<Comment> GetCommentsForRecordById(Guid id)
    {
        return _dbContext.Comments.Where(c => c.RecordId == id);
    }
}