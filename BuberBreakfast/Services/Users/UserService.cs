using BuberBreakfast.Models;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace BuberBreakfast.Services.Users
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _appDbContext;

        public UserService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ErrorOr<Created>> CreateUser(AppUser user)
        {
            _appDbContext.AppUsers.Add(user);
            await _appDbContext.SaveChangesAsync();

            return Result.Created;
        }

        public async Task<ErrorOr<List<AppUser>>> GetAllUsers()
        {
            List<AppUser> List = await _appDbContext.AppUsers.Select(user => new AppUser
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                CreatedDate = user.CreatedDate
                
            }).ToListAsync();

            if (List.Count == 0)
            {
                return Errors.User.NotFound;
            }

            return List;
        }

        public async Task<ErrorOr<AppUser>> GetUser(Guid userId)
        {
            var user = await _appDbContext.AppUsers.Select(user => new AppUser
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                CreatedDate = user.CreatedDate
            }).FirstOrDefaultAsync(user => user.Id == userId);

            if (user == null)
            {
                return Errors.User.NotFound;
            }

            return user;
        }

        public async Task<ErrorOr<UpdatedUser>> UpdateUser(AppUser user)
        {
            var entity = await _appDbContext.AppUsers.FirstOrDefaultAsync(u => u.Id == user.Id);

            bool isNewlyCreated = false;
            if (entity == null)
            {
                isNewlyCreated = true;
                _appDbContext.AppUsers.Add(user);
            }
            else
            {
                entity.FirstName = user.FirstName;
                entity.LastName = user.LastName;
                entity.Email = user.Email;
                entity.PhoneNumber = user.PhoneNumber;
            }

            await _appDbContext.SaveChangesAsync();

            return new UpdatedUser(isNewlyCreated);
        }
    }
}