using BuberBreakfast.Contracts.Post;
using ErrorOr;

namespace BuberBreakfast.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public AppUser User { get; set; }
        public Guid UserId { get; set; }
        public List<Comment> Comments { get; set; }


        public static ErrorOr<Post> Create(
            string title,
            string description,
            string imageUrl,
            DateTime createdDate,
            DateTime updatedDate,
            Guid userId,
            Guid? id = null
        )
        {
            List<Error> errors = new();

            if (errors.Any())
            {
                return errors;
            }

            return new Post
            {
                Id = id ?? Guid.NewGuid(),
                Title = title,
                Description = description,
                ImageUrl = imageUrl,
                CreatedDate = createdDate,
                UpdatedDate = updatedDate,
                UserId = userId,
            };
        }

        public static ErrorOr<Post> From(CreatePostRequest request)
        {
            return Create(
                request.Title,
                request.Description,
                request.ImageUrl,
                DateTime.UtcNow,
                DateTime.UtcNow,
                request.UserId
            );
        }
    }
}