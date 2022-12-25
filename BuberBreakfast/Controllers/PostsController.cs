﻿using BuberBreakfast.Contracts.Post;
using BuberBreakfast.Models;
using BuberBreakfast.Services.Posts;
using BuberBreakfast.Services.Users;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Controllers
{
    public class PostsController : ApiController
    {
        private readonly IPostService _postService;
        private readonly IUserService _userService;

        public PostsController(IPostService postService, IUserService userService)
        {
            _postService = postService;
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostRequest request)
        {
            ErrorOr<AppUser> user = await _userService.GetUser(request.userId);

            if (user.IsError)
            {
                return Problem(user.Errors);
            }

            ErrorOr<Post> requestToPostResult = Post.Create(request.Title, request.Description, request.ImageUrl, DateTime.UtcNow, DateTime.UtcNow, request.userId);

            if (requestToPostResult.IsError)
            {
                return Problem(requestToPostResult.Errors);
            }

            var post = requestToPostResult.Value;

            ErrorOr<Created> createPostResult = await _postService.CreatePost(post);

            return createPostResult.Match(craeted => Ok(MapPostResponse(post)), Problem);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            ErrorOr<List<Post>> posts = await _postService.GetAllPosts();


            if (posts.IsError) return Problem(posts.Errors);

            return Ok(posts.Value);
        }

        [HttpGet("user_posts/{id:guid}")]
        public async Task<IActionResult> GetUserPosts(Guid id)
        {
            ErrorOr<List<Post>> posts = await _postService.GetUserPosts(id);

            if (posts.IsError) return Problem(posts.Errors);

            return Ok(posts.Value);
        }

        [HttpGet("post_details/{id:guid}")]
        public async Task<IActionResult> GetPostDetails(Guid id)
        {

            return null;
        }

        [NonAction]
        private static PostResponse MapPostResponse(Post post)
        {

            return new PostResponse(
                post.Id,
                post.Title,
                post.Description,
                post.ImageUrl,
                post.CreatedDate,
                post.UpdatedDate,
                post.UserId
            );
        }

        [NonAction]
        private CreatedAtActionResult CreatedAtGetPost(Post post)
        {
            return CreatedAtAction(
                actionName: nameof(GetPostDetails),
                routeValues: new { id = post.Id },
                value: MapPostResponse(post)
            );
        }
    }
}
