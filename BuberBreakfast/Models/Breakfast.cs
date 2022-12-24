using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Services;
using ErrorOr;

namespace BuberBreakfast.Models;

public class Breakfast
{
    public const int MinNameLength = 3;
    public const int MaxNameLength = 50;

    public const int MinDescriptionLength = 50;
    public const int MaxDescriptionLength = 150;

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public DateTime LastModificationDateTime { get; set; }
    public string Savory { get; set; }
    public string Sweet { get; set; }

    public static ErrorOr<Breakfast> Create(
        string name,
        string description,
        DateTime startDateTime,
        DateTime endDateTime,
        string savory,
        string sweet,
        Guid? id = null
    )
    {
        List<Error> errors = new();
        if (name.Length is < MinNameLength or > MaxNameLength)
        {
            errors.Add(Errors.Breakfast.InvalidName);
        }

        if (description.Length is < MinDescriptionLength or > MaxDescriptionLength)
        {
            errors.Add(Errors.Breakfast.InvalidDescription);
        }

        if (errors.Any())
        {
            return errors;
        }

        return new Breakfast
        {
            Id = id ?? Guid.NewGuid(),
            Name = name,
            Description = description,
            StartDateTime = startDateTime,
            EndDateTime = endDateTime,
            LastModificationDateTime = DateTime.UtcNow,
            Savory = savory,
            Sweet = sweet
        };
    }

    public static ErrorOr<Breakfast> From(CreateBreakfastRequest request)
    {
        return Create(
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            request.Savory,
            request.Sweet
        );
    }

    public static ErrorOr<Breakfast> From(Guid id, UpsertBreakfastRequest request)
    {
        return Create(
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            request.Savory,
            request.Sweet,
            id
        );
    }
}