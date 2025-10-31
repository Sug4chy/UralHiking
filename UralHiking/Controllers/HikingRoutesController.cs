using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UralHiking.Database;
using UralHiking.Models;
using UralHiking.Models.Dto.Requests;
using UralHiking.Models.Enums;

namespace UralHiking.Controllers;

[ApiController]
[Route("/api/hiking-routes")]
public class HikingRoutesController : ControllerBase
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
}