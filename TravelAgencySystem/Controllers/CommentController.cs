using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelAgencySystemCore.Helpers.Auth;
using TravelAgencySystemCore.Interfaces.Service;
using TravelAgencySystemCore.Requests.Comment;

namespace TravelAgencySystem.Controllers;

public class CommentController : BaseController
{
    private readonly ICommentService _commentService;
    
    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }
    
    [Authorize(Policy = PolicyNames.ClientRole)]
    [HttpGet("{id:guid}")]
    public IActionResult Get(Guid id)
    {
        return Ok(_commentService.GetCommentById(id));
    }
    
    [Authorize(Policy = PolicyNames.ClientRole)]
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_commentService.GetComments());
    }
    
    [Authorize(Policy = PolicyNames.ClientRole)]
    [HttpPost]
    public IActionResult Post(CommentRequest request)
    {
        var res = _commentService.AddComment(request, User.FindFirstValue(ClaimTypes.NameIdentifier));
        return CreatedAtAction(nameof(Get), new { id = res }, res);
    }
    
    [Authorize(Policy = PolicyNames.ClientRole)]
    [HttpPut("{id:guid}")]
    public IActionResult Put(Guid id, CommentRequest request)
    {
        var clientId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var res = _commentService.EditComment(id, request, clientId);
        return Ok(res);
    }
    
    [Authorize(Policy = PolicyNames.ClientRole)]
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var clientId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Ok(_commentService.DeleteComment(id, clientId));
    }
}