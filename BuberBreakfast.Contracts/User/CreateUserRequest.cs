namespace BuberBreakfast.Contracts.User
{
    public record CreateUserRequest(
        string FirstName,
        string LastName,
        string PhoneNumber,
        string Email
    );
}
