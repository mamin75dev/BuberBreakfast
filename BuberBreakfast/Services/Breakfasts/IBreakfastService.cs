using BuberBreakfast.Models;
using ErrorOr;

namespace BuberBreakfast.Services.Breakfasts;

public interface IBreakfastService
{
    Task<ErrorOr<Created>> CreateBreakfast(Breakfast breakfast);
    Task<ErrorOr<Breakfast>> GetBreakfast(Guid id);
    Task<ErrorOr<UpsertedBreakfast>> UpsertBreakfast(Breakfast breakfast);
    Task<ErrorOr<Deleted>> DeleteBreakfast(Guid id);
    Task<ErrorOr<List<Breakfast>>> GetAllBreakfasts();
}