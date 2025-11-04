using Microsoft.AspNetCore.Mvc;

namespace UralHiking.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class MyamyamyaController : ControllerBase
{
    [HttpGet]
    public IActionResult Index([FromServices] IWebHostEnvironment environment)
    {
        return Ok(new
        {
            environment.ContentRootPath, environment.WebRootPath,
        });
    }
}