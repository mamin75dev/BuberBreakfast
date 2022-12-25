using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models;
using BuberBreakfast.Services.Breakfasts;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Controllers;

public class BreakfastsController : ApiController
{
    private readonly IBreakfastService _breakfastService;

    public BreakfastsController(IBreakfastService breakfastService)
    {
        _breakfastService = breakfastService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBreakfast(CreateBreakfastRequest request)
    {
        ErrorOr<Breakfast> requestToBreakfastResult = Breakfast.From(request);

        if (requestToBreakfastResult.IsError)
        {
            return Problem(requestToBreakfastResult.Errors);
        }

        var breakfast = requestToBreakfastResult.Value;

        ErrorOr<Created> createBreakfastResult = await _breakfastService.CreateBreakfast(breakfast);

        return createBreakfastResult.Match(created => CreatedAtGetBreakfast(breakfast), Problem);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBreakfasts()
    {
        ErrorOr<List<Breakfast>> getBreakfastsResult = await _breakfastService.GetAllBreakfasts();

        return getBreakfastsResult.Match(breakfasts => Ok(breakfasts), Problem);

    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetBreakfast(Guid id)
    {
        ErrorOr<Breakfast> getBreakfastResult = await _breakfastService.GetBreakfast(id);

        return getBreakfastResult.Match(breakfast => Ok(MapBreakfastResponse(breakfast)), Problem);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpsertBreakfast(Guid id, UpsertBreakfastRequest request)
    {
        ErrorOr<Breakfast> requestToBreakfastResult = Breakfast.From(id, request);

        if (requestToBreakfastResult.IsError)
        {
            return Problem(requestToBreakfastResult.Errors);
        }

        var breakfast = requestToBreakfastResult.Value;

        ErrorOr<UpsertedBreakfast> upsertBreakfastResult = await _breakfastService.UpsertBreakfast(breakfast);

        return upsertBreakfastResult.Match(
            updated => updated.IsNewlyCreated ? CreatedAtGetBreakfast(breakfast) : NoContent(), Problem);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteBreakfast(Guid id)
    {
        ErrorOr<Deleted> deleteBreakfastResult = await _breakfastService.DeleteBreakfast(id);

        return deleteBreakfastResult.Match(updated => NoContent(), Problem);
    }
    [NonAction]
    private static BreakfastResponse MapBreakfastResponse(Breakfast breakfast)
    {
        return new BreakfastResponse(
            breakfast.Id,
            breakfast.Name,
            breakfast.Description,
            breakfast.StartDateTime,
            breakfast.EndDateTime,
            breakfast.LastModificationDateTime,
            breakfast.Savory,
            breakfast.Sweet
        );
    }
    [NonAction]
    private CreatedAtActionResult CreatedAtGetBreakfast(Breakfast breakfast)
    {
        return CreatedAtAction(
            actionName: nameof(GetBreakfast),
            routeValues: new { id = breakfast.Id },
            value: MapBreakfastResponse(breakfast)
        );
    }
}