using BuberBreakfast.Models;
using ErrorOr;

namespace BuberBreakfast.Services.Posts
{
    public interface IPostService
    {
        Task<ErrorOr<Created>> CreatePost(Post post);

        Task<ErrorOr<List<Post>>> GetUserPosts(Guid userId);
        Task<ErrorOr<List<Post>>> GetAllPosts();
        Task<ErrorOr<Post>> GetPostDetails(Guid postId);
        Task<ErrorOr<UpdatedPostResult>> UpdatePost(Post post);
        Task<ErrorOr<Deleted>> DeletePost(Guid id);
    }
}
