namespace BuberBreakfast.Contracts.Post
{
    public record CreatePostRequest(
        string Title,
        string Description,
        string ImageUrl,
        Guid userId
    );
}
