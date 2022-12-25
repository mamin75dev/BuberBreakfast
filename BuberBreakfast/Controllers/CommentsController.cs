using BuberBreakfast.Contracts.Post;
using BuberBreakfast.Models;
using BuberBreakfast.Services.Comments;
using BuberBreakfast.Services.Posts;
using BuberBreakfast.Services.Users;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Controllers;

public class CommentsController : ApiController
{
    private readonly IUserService _userService;
    private readonly IPostService _postService;
    private readonly ICommentService _commentService;

    public CommentsController(IUserService userService, IPostService postService, ICommentService commentService)
    {
        _userService = userService;
        _postService = postService;
        _commentService = commentService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateComment(CreateCommentRequest request)
    {
        ErrorOr<AppUser> user = await _userService.GetUser(request.UserId);

        if (user.IsError) return Problem(user.Errors);

        ErrorOr<Post> post = await _postService.GetPostDetails(request.PostId);

        if (post.IsError) return Problem(post.Errors);

        ErrorOr<Comment> requestToCommentResult = Comment.From(request);

        if (requestToCommentResult.IsError) return Problem(requestToCommentResult.Errors);

        var comment = requestToCommentResult.Value;

        ErrorOr<Created> createCommentResult = await _commentService.CreateComment(comment);

        return createCommentResult.Match(created => Ok(MapCommentResponse(comment)), Problem);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteComment([FromQuery(Name = "id")] Guid id)
    {
        ErrorOr<Deleted> deleteCommentResult = await _commentService.DeleteComment(id);

        return deleteCommentResult.Match(updated => NoContent(), Problem);
    }

    [NonAction]
    public static CommentResponse MapCommentResponse(Comment comment)
    {
        return new CommentResponse(comment.Id, comment.Text, comment.CreatedDate, comment.PostId, comment.UserId);
    }
}