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

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(CreateUserRequest request)
        {
            ErrorOr<AppUser> requestToUserResult = AppUser.From(request);

            if (requestToUserResult.IsError)
            {
                return Problem(requestToUserResult.Errors);
            }

            var user = requestToUserResult.Value;

            ErrorOr<Created> createBreakfastResult = await _userService.CreateUser(user);

            return createBreakfastResult.Match(created => CreatedAtGetUser(user), Problem);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            ErrorOr<List<AppUser>> users = await _userService.GetAllUsers();

            if (users.IsError) return Problem(users.Errors);

            return Ok(users.Value);
        }

        [HttpGet("details")]
        public async Task<IActionResult> GetUser([FromQuery(Name = "id")] Guid id)
        {
            ErrorOr<AppUser> getUserResult = await _userService.GetUser(id);

            return getUserResult.Match(user => Ok(MapUserResponse(user)), Problem);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromQuery(Name = "id")] Guid id, UpdateUserRequest request)
        {
            ErrorOr<AppUser> requestToUserResult = AppUser.From(id, request);

            if (requestToUserResult.IsError)
            {
                return Problem(requestToUserResult.Errors);
            }

            var appUser = requestToUserResult.Value;

            ErrorOr<UpdatedUser> updateUserResult = await _userService.UpdateUser(appUser);

            return updateUserResult.Match(updated => updated.IsNewlyCreated ? CreatedAtGetUser(appUser) : NoContent(),
                Problem);
        }

        [NonAction]
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
        private CreatedAtActionResult CreatedAtGetUser(AppUser user)
        {
            return CreatedAtAction(
                actionName: nameof(GetUser),
                routeValues: new { id = user.Id },
                value: MapUserResponse(user)
            );
        }
    }
}