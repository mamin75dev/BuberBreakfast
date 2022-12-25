using BuberBreakfast.Contracts.Post;
using ErrorOr;

namespace BuberBreakfast.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public Guid UserId { get; set; }
        public AppUser User { get; set; }

        public static ErrorOr<Comment> Create(
            string text,
            Guid userId,
            Guid postId
        )
        {
            List<Error> errors = new();

            if (errors.Any())
            {
                return errors;
            }

            return new Comment { Text = text, CreatedDate = DateTime.UtcNow, UserId = userId, PostId = postId };
        }

        public static ErrorOr<Comment> From(CreateCommentRequest request)
        {
            return Create(request.Text, request.UserId, request.PostId);
        }
    }
}
