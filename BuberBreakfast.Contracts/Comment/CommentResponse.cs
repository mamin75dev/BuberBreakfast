namespace BuberBreakfast.Contracts.Post;

public record CommentResponse(Guid Id, string Text, DateTime CreatedDate, Guid PostId, Guid UserId);