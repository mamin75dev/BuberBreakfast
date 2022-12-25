namespace BuberBreakfast.Contracts.Post
{
    public record UpdateUserRequest(
        string Title,
        string Description,
        string ImageUrl,
        Guid userId
    );
}
