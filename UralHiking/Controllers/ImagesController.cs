using Microsoft.AspNetCore.Mvc;

namespace UralHiking.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ImagesController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;
    private string WebRootImagesPath => Path.Combine(_environment.WebRootPath, "images");

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
        await using var fs = System.IO.File.OpenWrite(Path.Combine(WebRootImagesPath, fileName) );
        await photo.CopyToAsync(fs, ct);
        await fs.FlushAsync(ct);

        return Created($"{Request.Scheme}://{Request.Host}{Request.PathBase}/images/{fileName}", null);
    }

    [HttpDelete("fileName")]
    public IActionResult DeleteFile([FromRoute] string fileName, CancellationToken ct = default)
    {
        string absoluteFilePath = Path.Combine(WebRootImagesPath, fileName);
        if (!System.IO.File.Exists(absoluteFilePath))
        {
            return NotFound("File with passed name wasn't found");
        }

        System.IO.File.Delete(absoluteFilePath);

        return NoContent();
    }
}