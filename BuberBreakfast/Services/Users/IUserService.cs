using BuberBreakfast.Models;
using ErrorOr;

namespace BuberBreakfast.Services.Users
{
    public interface IUserService
    {
        Task<ErrorOr<Created>> CreateUser(AppUser user);
        Task<ErrorOr<AppUser>> GetUser(Guid userId);
        Task<ErrorOr<UpdatedUser>> UpdateUser(AppUser user);
    }
}
