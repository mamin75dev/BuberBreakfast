namespace BuberBreakfast.Contracts.Post;

public record CreateCommentRequest(string Text, Guid UserId, Guid PostId);