using BuberBreakfast.Models;
using ErrorOr;

namespace BuberBreakfast.Services.Comments
{
    public class CommentService : ICommentService
    {
        private readonly AppDbContext _appDbContext;

        public CommentService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ErrorOr<Created>> CreateComment(Comment comment)
        {
            _appDbContext.Comments.Add(comment);
            await _appDbContext.SaveChangesAsync();

            return Result.Created;
        }

        public async Task<ErrorOr<Deleted>> DeleteComment(Guid id)
        {
            var entity = new Comment { Id = id };

            _appDbContext.Attach(entity);
            _appDbContext.Remove(entity);

            await _appDbContext.SaveChangesAsync();

            return Result.Deleted;
        }
    }
}
