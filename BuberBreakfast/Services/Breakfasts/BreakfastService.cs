using BuberBreakfast.Models;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace BuberBreakfast.Services.Breakfasts;

public class BreakfastService : IBreakfastService
{
    private readonly AppDbContext _appDbContext;

    public BreakfastService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<ErrorOr<Created>> CreateBreakfast(Breakfast breakfast)
    {

        _appDbContext.Breakfasts.Add(breakfast);
        await _appDbContext.SaveChangesAsync();

        return Result.Created;
    }

    public async Task<ErrorOr<Breakfast>> GetBreakfast(Guid id)
    {

        Breakfast breakfast = await _appDbContext.Breakfasts.Select(breakfast => new Breakfast
        {
            Id = breakfast.Id,
            Name = breakfast.Name,
            Description = breakfast.Description,
            StartDateTime = breakfast.StartDateTime,
            EndDateTime = breakfast.EndDateTime,
            LastModificationDateTime = breakfast.LastModificationDateTime,
            Savory = breakfast.Savory,
            Sweet = breakfast.Sweet,
        }).FirstOrDefaultAsync(br => br.Id == id);

        if (breakfast == null)
        {
            return Errors.Breakfast.NotFound;
        }

        return breakfast;

    }

    public async Task<ErrorOr<UpsertedBreakfast>> UpsertBreakfast(Breakfast breakfast)
    {

        var entity = await _appDbContext.Breakfasts.FirstOrDefaultAsync(br => br.Id == breakfast.Id);

        bool IsNewlyCreated = false;
        if (entity == null)
        {
            IsNewlyCreated = true;
            _appDbContext.Breakfasts.Add(breakfast);
        }
        else
        {
            entity.Name = breakfast.Name;
            entity.Description = breakfast.Description;
            entity.StartDateTime = breakfast.StartDateTime;
            entity.EndDateTime = breakfast.EndDateTime;
            entity.LastModificationDateTime = DateTime.UtcNow;
            entity.Savory = breakfast.Savory;
            entity.Sweet = breakfast.Sweet;
        }

        await _appDbContext.SaveChangesAsync();

        return new UpsertedBreakfast(IsNewlyCreated);
    }

    public async Task<ErrorOr<Deleted>> DeleteBreakfast(Guid id)
    {
        var entity = new Breakfast { Id = id };

        _appDbContext.Attach(entity);
        _appDbContext.Remove(entity);

        await _appDbContext.SaveChangesAsync();

        return Result.Deleted;
    }

    public async Task<ErrorOr<List<Breakfast>>> GetAllBreakfasts()
    {
        List<Breakfast> List = await _appDbContext.Breakfasts.Select(breakfast => new Breakfast
        {
            Id = breakfast.Id,
            Name = breakfast.Name,
            Description = breakfast.Description,
            StartDateTime = breakfast.StartDateTime,
            EndDateTime = breakfast.EndDateTime,
            LastModificationDateTime = breakfast.LastModificationDateTime,
            Savory = breakfast.Savory,
            Sweet = breakfast.Sweet,
        }).ToListAsync();

        if (List.Count == 0)
        {
            return Errors.Breakfast.NotFound;
        }

        return List;
    }
}