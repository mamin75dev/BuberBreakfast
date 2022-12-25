using BuberBreakfast.Models;
using ErrorOr;

namespace BuberBreakfast.Services.Comments
{
    public interface ICommentService
    {
        Task<ErrorOr<Created>> CreateComment(Comment comment);
        Task<ErrorOr<Deleted>> DeleteComment(Guid id);
    }
}
