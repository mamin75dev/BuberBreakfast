using ErrorOr;

namespace BuberBreakfast.Services;

public class Errors
{
    public static class Breakfast
    {
        public static Error InvalidName =>
            Error.Validation(code: "Breakfast.InvalidName!", description: "Breakfast length is invalid!");

        public static Error InvalidDescription =>
            Error.Validation(code: "Description.InvalidName!", description: "Description length is invalid!");

        public static Error NotFound =>
            Error.NotFound(code: "Breakfast.NotFound!", description: "Breakfast not found!");
    }

    public static class User
    {
        public static Error InvalidPhoneNumber =>
            Error.NotFound(code: "User.InvalidPhoneNumber!", description: "Phone number is invalid");
        public static Error InvalidEmail =>
            Error.NotFound(code: "User.InvalidEmail!", description: "Email is invalid");
        public static Error NotFound =>
            Error.NotFound(code: "User.NotFound!", description: "User not found!");
    }

    public static class Post
    {
        public static Error NotFound => Error.NotFound(code: "Post.NotFound!", description: "Post Not Found!");
    }

    public static class Comment
    {
        public static Error NotFound => Error.NotFound(code: "Comment.NotFound!", description: "Comment Not Found!");
    }
}