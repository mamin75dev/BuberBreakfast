namespace BuberBreakfast.Contracts.Post
{
    public record PostResponse(Guid id, string Title, string Description, string ImageUrl, DateTime CreatedDate, DateTime UpdatedDate, Guid ForUserId);
}
