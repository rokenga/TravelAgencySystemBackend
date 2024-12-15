using TravelAgencySystemDomain.Entities;

namespace TravelAgencySystemCore.Interfaces.Repository;

public interface ICommentRepo
{
    Comment GetCommentById(Guid id);
    IEnumerable<Comment> GetComments();
    Guid AddComment(Comment comment);
    bool EditComment(Comment comment);
    bool DeleteComment(Guid guid);
    bool CommentExists(Guid guid);
    IEnumerable<Comment> GetCommentsForRecordById(Guid id);
}