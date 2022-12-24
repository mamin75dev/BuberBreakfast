namespace BuberBreakfast.Contracts.User
{
    public record UserResponse(
        Guid Id,
        string Name,
        string PhoneNumber,
        string Email,
        DateTime CreatedDate
    );
}
