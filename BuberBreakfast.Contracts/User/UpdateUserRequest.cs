namespace BuberBreakfast.Contracts.User
{
    public record UpdateUserRequest(
        string FirstName,
        string LastName,
        string PhoneNumber,
        string Email
    );
}
