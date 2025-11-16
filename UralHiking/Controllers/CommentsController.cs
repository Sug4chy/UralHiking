using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UralHiking.Database;
using UralHiking.Models;
using UralHiking.Models.Dto.Requests;

namespace UralHiking.Controllers;

[ApiController]
[Route("/api/hiking-routes/{routeId:int}/comments")]
public sealed class CommentsController : ControllerBase
{
    private readonly DatabaseContext _dbContext;

    public CommentsController(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Read(
        [FromRoute] int routeId,
        CancellationToken ct = default)
    {
        if (!await _dbContext.HikingRoutes.AnyAsync(x => x.Id == routeId, ct))
        {
            return NotFound("Route with passed ID wasn't found");
        }

        return Ok(await _dbContext.Comments
            .Where(x => x.HikingRouteId == routeId)
            .ToArrayAsync(ct));
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromRoute] int routeId,
        [FromBody] CreateCommentRequest request,
        CancellationToken ct = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var route = await _dbContext.HikingRoutes.FirstOrDefaultAsync(x => x.Id == routeId, ct);
        if (route is null)
        {
            return NotFound("Route with passed ID wasn't found");
        }

        route.Comments.Add(
            new Comment
            {
                Content = request.Content,
                HikingRouteId = routeId,
                HikingRoute = route,
                UserLogin = request.UserLogin,
                UserEmail = request.UserEmail,
            }
        );

        return CreatedAtAction("Read", new { id = routeId }, null);
    }
}