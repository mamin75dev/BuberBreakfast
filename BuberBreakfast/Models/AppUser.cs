using BuberBreakfast.Contracts.User;
using BuberBreakfast.Services;
using ErrorOr;

namespace BuberBreakfast.Models
{
    public class AppUser
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<Post> Posts { get; set; }
        public List<Comment> Comments { get; set; }

        public static ErrorOr<AppUser> Create(
            string firstName,
            string lastName,
            string phoneNumber,
            string email,
            Guid? id = null
        )
        {
            List<Error> errors = new();
            if (phoneNumber.Length != 11)
            {
                errors.Add(Errors.User.InvalidPhoneNumber);
            }
            if (!email.Contains("@") || !email.Contains("."))
            {
                errors.Add(Errors.User.InvalidEmail);
            }

            if (errors.Any())
            {
                return errors;
            }

            return new AppUser
            {
                Id = id ?? Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                Email = email,
            };
        }

        public static ErrorOr<AppUser> From(CreateUserRequest request)
        {
            return Create(
                request.FirstName,
                request.LastName,
                request.PhoneNumber,
                request.Email
            );
        }

        public static ErrorOr<AppUser> From(Guid id, CreateUserRequest request)
        {
            return Create(
                request.FirstName,
                request.LastName,
                request.PhoneNumber,
                request.Email,
                id
            );
        }
    }
}
