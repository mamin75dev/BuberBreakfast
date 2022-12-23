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
}