using Microsoft.AspNetCore.Mvc;

namespace UralHiking.Controllers;

[ApiController]
[Route("api/static-files")]
public sealed class StaticFilesController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;

    public StaticFilesController(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    [HttpPost]
    public async Task<IActionResult> UploadHikingRoutePhoto(
        IFormFile photo,
        CancellationToken ct = default)
    {
        if (!photo.ContentType.StartsWith("image/"))
        {
            return BadRequest("File must be an image file.");
        }

        string fileName = Path.GetRandomFileName() + Path.GetExtension(photo.FileName);
        await using var fs = System.IO.File.OpenWrite(
            Path.Combine(_environment.WebRootPath, "images", fileName)
        );
        await photo.CopyToAsync(fs, ct);
        await fs.FlushAsync(ct);

        return Created($"{Request.Scheme}://{Request.Host}{Request.PathBase}/images/{fileName}", null);
    }
}