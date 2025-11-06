using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UralHiking.Database;
using UralHiking.Models;
using UralHiking.Models.Dto.Requests;
using UralHiking.Models.Enums;

namespace UralHiking.Controllers;

[ApiController]
[Route("/api/hiking-routes")]
public sealed class HikingRoutesController : ControllerBase
{
    private readonly DatabaseContext _dbContext;

    public HikingRoutesController(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    [NonAction]
    private static HikingRouteDifficulty? ParseDifficulty(string difficulty)
        => difficulty switch
        {
            "EASY" => HikingRouteDifficulty.Easy,
            "MODERATE" => HikingRouteDifficulty.Moderate,
            "HARD" => HikingRouteDifficulty.Hard,
            _ => null
        };

    [HttpGet]
    [ProducesResponseType(typeof(HikingRoute[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken ct = default)
    {
        var result = await _dbContext.HikingRoutes
            .Include(x => x.Coordinates)
            .Include(x => x.GearItems)
            .ToArrayAsync(ct);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateHikingRouteRequest request, CancellationToken ct = default)
    {
        var difficulty = ParseDifficulty(request.Difficulty);
        if (difficulty is null)
        {
            return BadRequest("Cannot parse difficulty");
        }

        var route = new HikingRoute
        {
            Name = request.Name,
            ShortDescription = request.ShortDescription,
            Description = request.Description,
            DistanceMeters = request.DistanceMeters,
            DurationMinutes = request.DurationMinutes,
            DifficultyInternal = difficulty.Value,
            AscentMeters = request.AscentMeters,
            Rating = request.Rating,
            ReviewCount = request.ReviewCount,
            PhotoUrl = request.PhotoUrl,
            LocationName = request.LocationName,
            GearItems = request.GearItems.Select(x => new GearItem { Text = x.Text, Url = x.Url }).ToList(),
            Coordinates = request.Coordinates
                .Select(x => new Coordinate { Latitude = x.Latitude, Longitude = x.Longitude })
                .ToList()
        };

        _dbContext.HikingRoutes.Add(route);
        await _dbContext.SaveChangesAsync(ct);

        return Created($"{Request.Scheme}://{Request.Host}{Request.PathBase}/api/hiking-routes", null);
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> Edit(
        [FromRoute] int id, 
        [FromBody] EditHikingRouteRequest request,
        CancellationToken ct = default)
    {
        var route = await _dbContext.HikingRoutes.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (route is null)
        {
            return NotFound("Route with passed ID wasn't found");
        }

        if (request.Name is not null) route.Name = request.Name;
        if (request.ShortDescription is not null) route.ShortDescription = request.ShortDescription;
        if (request.Description is not null) route.Description = request.Description;
        if (request.DistanceMeters.HasValue) route.DistanceMeters = request.DistanceMeters.Value;
        if (request.DurationMinutes.HasValue) route.DurationMinutes = request.DurationMinutes.Value;
        if (request.AscentMeters.HasValue) route.AscentMeters = request.AscentMeters.Value;
        if (request.Rating.HasValue) route.Rating = request.Rating.Value;
        if (request.ReviewCount.HasValue) route.ReviewCount = request.ReviewCount.Value;
        if (request.PhotoUrl is not null) route.PhotoUrl = request.PhotoUrl;
        if (request.LocationName is not null) route.LocationName = request.LocationName;
        if (request.Difficulty is not null)
        {
            var parsedDifficulty = ParseDifficulty(request.Difficulty);
            if (parsedDifficulty is null)
            {
                return BadRequest("Cannot parse difficulty");
            }

            route.DifficultyInternal = parsedDifficulty.Value;
        }

        if (request.GearItems is not null)
        {
            route.GearItems = request.GearItems.Select(x => new GearItem { Text = x.Text, Url = x.Url }).ToList();
        }

        if (request.Coordinates is not null)
        {
            route.Coordinates = request.Coordinates
                .Select(x => new Coordinate { Latitude = x.Latitude, Longitude = x.Longitude })
                .ToList();
        }

        await _dbContext.SaveChangesAsync(ct);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken ct = default)
    {
        var route = await _dbContext.HikingRoutes.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (route is null)
        {
            return NotFound("Route with passed ID wasn't found");
        }

        _dbContext.HikingRoutes.Remove(route);
        await _dbContext.SaveChangesAsync(ct);

        return NoContent();
    }
}