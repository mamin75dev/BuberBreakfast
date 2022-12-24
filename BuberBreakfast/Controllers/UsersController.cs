using BuberBreakfast.Contracts.User;
using BuberBreakfast.Models;
using BuberBreakfast.Services.Users;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserRequest request)
        {
            ErrorOr<AppUser> requestToUserResult = AppUser.From(request);

            if (requestToUserResult.IsError)
            {
                return Problem(requestToUserResult.Errors);
            }

            var user = requestToUserResult.Value;

            ErrorOr<Created> createBreakfastResult = await _userService.CreateUser(user);

            return createBreakfastResult.Match(created => CreatedAtGetBreakfast(user), Problem);
        }


        private static UserResponse MapUserResponse(AppUser user)
        {
            var userName = user.FirstName + " " + user.LastName;

            return new UserResponse(
                user.Id,
                userName,
                user.PhoneNumber,
                user.Email,
                user.CreatedDate
            );
        }
        [NonAction]
        private CreatedAtActionResult CreatedAtGetBreakfast(AppUser user)
        {
            return CreatedAtAction(
                actionName: "",
                routeValues: new { id = user.Id },
                value: MapUserResponse(user)
            );
        }
    }
}
