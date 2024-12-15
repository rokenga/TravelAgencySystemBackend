using TravelAgencySystemCore.Requests.Comment;
using TravelAgencySystemCore.Responses.Comment;
using TravelAgencySystemDomain.Entities;

namespace TravelAgencySystemCore.Interfaces.Service;

public interface ICommentService
{
    CommentResponse GetCommentById(Guid guid);
    IEnumerable<Comment> GetComments();
    Guid AddComment(CommentRequest comment, string clientId);
    bool EditComment(Guid guid, CommentRequest comment, string clientId);
    bool DeleteComment(Guid guid, string clientId);
}