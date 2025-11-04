using Microsoft.AspNetCore.Mvc;

namespace UralHiking.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ImagesController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;

    public ImagesController(IWebHostEnvironment environment)
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

        return Created($"https://{Request.Host}{Request.PathBase}/images/{fileName}", null);
    }
}